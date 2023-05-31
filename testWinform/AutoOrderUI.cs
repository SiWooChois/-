using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Data.Common;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace testWinform
{
    internal partial class AutoOrderUI : Form
    {

        // 초기화 할 field
        private OrderSystem orderSystem; // 발주 시스템
        private searchProductSystem searchProductSystem; // 검색 시스템
        private ConditionalOrder condition;
        string selectedDate = "-";
        int count;
        // 기본 열 추가할 배열
        string[] orderColumnNames = { "", "NO", "상품코드", "상품명", "거래처", "규격", "현재수량", "빌주수량", "단가", "공급가액", "부가세", "합계", "발주 일자" };
        public AutoOrderUI()
        {
            InitializeComponent();
            //객체 초기화는 필수, 안 하면 개체 참조 오류 뜸
            orderSystem = new OrderSystem(this);
            searchProductSystem = new searchProductSystem(this);
            condition = new ConditionalOrder(0, 0, 0, 0);
            selectedDate = order_Date.Value.ToString("yyyy-MM-dd");
            setCondition(); // 발주 조건 가져와서 세팅
            showOrderList(orderColumnNames, selectedDate); // 발주 목록 표시 
        }
        private void AutoOrderUI_Shown(object sender, EventArgs e)
        {
            setOrderList(0); // 자동 발주 계산
        }
        public void setCondition()
        {
            condition = orderSystem.getCondition();
            displayed_StockMin_textBox.Text = condition.getDisplayedStockMin().ToString(); // 표시할 재고량 최소 값
            displayed_StockMax_textBox.Text = condition.getDisplayedStockMax().ToString(); // 표시할 재고량 최대 값
            auto_StockMin_textBox.Text = condition.getAutoStockMin().ToString(); // 자동 발주될 표시할 재고량 최소값
            auto_Order_Quantity_textBox.Text = condition.getStockQuantity().ToString(); // 자동 발주될 재고수량
        }
        // orderList에 보여주는 함수. 
        public void showOrderList(string[] columnNames, string Date)
        {
            order_listView.Clear(); // 매번 초기화
            total_ListView.Clear(); // 합계 리스트 초기화
            // 기본적으로 보여지는 orderList 형식
            if (columnNames.Length == 13)
            {
                int[] columnWidth = { 0, 37, 100, 110, 112, 60, 80, 80, 80, 90, 90, 92, 160 };
                // 배열 만큼 열 생성
                for (int i = 0; i < columnNames.Length; i++)
                {
                    ColumnHeader column = new ColumnHeader(); // ColumnHeader 설정
                    column.TextAlign = HorizontalAlignment.Center;
                    column.Width = columnWidth[i];
                    column.Text = columnNames[i];
                    // ListView의 Columns 컬렉션에 열을 추가
                    order_listView.Columns.Add(column);
                }
                orderSystem.showOrderItems(readCondition(), Date);
            }

            // 상품 또는 거래처 검색 시 보여지는 orderList 형식
            else if (columnNames.Length == 10)
            {
                int[] columnWidth = { 0, 90, 220, 90, 120, 120, 120, 110, 60, 60 };
                // 배열 만큼 열 생성
                for (int i = 0; i < columnNames.Length; i++)
                {
                    ColumnHeader column = new ColumnHeader(); // ColumnHeader 설정
                    column.TextAlign = HorizontalAlignment.Center;
                    column.Width = columnWidth[i];
                    column.Text = columnNames[i];
                    // ListView의 Columns 컬렉션에 열을 추가
                    order_listView.Columns.Add(column);
                }
                if (columnNames[1] == "거래처")
                {
                    searchProductSystem.performSearch(manufacturing_company_textBox.Text, true, Date);
                }
                else if (columnNames[1] == "상품코드")
                {
                    searchProductSystem.performSearch(product_Name_textBox.Text, false, Date);
                }
            }
            else
            {
                MessageBox.Show("문제 발생");
            }
        }
        // 발주 조건 검출
        public ConditionalOrder readCondition()
        {
            // 표시할 재고량(미만)
            string StockMin = displayed_StockMin_textBox.Text;
            int DisplayedStockMin = int.Parse(StockMin);
            // 표시할 재고량(이상)
            string StockMax = displayed_StockMax_textBox.Text;
            int DisplayedStockMax = int.Parse(StockMax);
            // 자동 발주 수량(미만)
            string auto_StockMin = auto_StockMin_textBox.Text;
            int AutoStockMin = int.Parse(auto_StockMin);
            // 자동 발주될 수량(발주서)
            string auto_Order_Quantity = auto_Order_Quantity_textBox.Text;
            int StockQuantity = int.Parse(auto_Order_Quantity);

            // 발주 조건 생성자
            ConditionalOrder scondition = new ConditionalOrder(DisplayedStockMin, DisplayedStockMax, AutoStockMin, StockQuantity);
            return scondition;
        }
        // 표시할 orderListView 받아와서 넣기
        public void addOrderToListView(ListViewItem[] items, ListViewItem totalItem)
        {
            string totalsupplyValue = "";
            string totalVAT = "";
            string totalAmount = "";
            totalsupplyValue = totalItem.SubItems[1].Text;
            totalVAT = totalItem.SubItems[2].Text;
            totalAmount = totalItem.SubItems[3].Text;
            //if(count == 0)
            //{
            //    totalsupplyValue = totalItem.SubItems[1].Text;
            //    totalVAT = totalItem.SubItems[2].Text;
            //    totalAmount = totalItem.SubItems[3].Text;
            //    count++;
            //}
            //if (total_ListView.SelectedItems.Count > 0)
            //{

            //}               

            string[] totalValue = { "", "합계", totalsupplyValue, totalVAT, totalAmount };

            int[] total_column_width = { 0, 660, 90, 90, 92 };
            foreach (ListViewItem item in items)
            {
                order_listView.Items.Add(item);
            }
            for (int i = 0; i < 5; i++)
            {
                ColumnHeader column = new ColumnHeader();
                column.TextAlign = HorizontalAlignment.Center;
                column.Width = total_column_width[i];
                column.Text = totalValue[i];
                total_ListView.Columns.Add(column);
            }
        }
        public void setOrderList(int check)
        {
            int count = 0;
            Order[] orders = new Order[order_listView.Items.Count]; // order_listView의 아이템 수로 배열을 초기화
            foreach (ListViewItem item in order_listView.Items)
            {
                int order_ID = int.Parse(item.SubItems[1].Text); // 주문번호
                string productName = item.SubItems[3].Text; // 상품명
                string standard = item.SubItems[5].Text; // 규격
                int orderQuantity = int.Parse(item.SubItems[6].Text); // 발주수량
                int productPrice = int.Parse(item.SubItems[8].Text); // 상품 가격(단일)
                int supplyValue = int.Parse(item.SubItems[9].Text); // 공급가액
                int orderVAT = int.Parse(item.SubItems[10].Text); // 부가세
                int orderTotalValue = int.Parse(item.SubItems[11].Text); // 합계
                string orderDate = DateTime.Now.ToString("yyyy-MM-dd"); // 발주일자

                orders[count] = new Order(order_ID, productName, standard, orderQuantity, productPrice, supplyValue, orderVAT, orderTotalValue, orderDate); // 새 Order 객체 생성 및 필드 초기화
                count++;
            }
            orderSystem.Order(readCondition(), orders, check);
        }
        // 발주 버튼 클릭
        private void order_button_Click(object sender, EventArgs e)
        {
            setOrderList(1);
        }
        // 거래처로 검색
        private void manufacturing_company_textBox_TextChanged(object sender, EventArgs e)
        {
            // 반복 시 보여지는 목록 리스트 열 헤더
            string[] manufacturingColumnNames = { "", "거래처", "상품코드", "상품명", "규격", "단가", "재고수량", "공급업체", "판매", "품절" };

            // 검색어가 입력되었을 경우에만 showOrderList 메서드 호출
            if (!string.IsNullOrEmpty(manufacturing_company_textBox.Text))
            {
                showOrderList(manufacturingColumnNames, selectedDate);
            }
            else
            {
                // 검색어가 비어있을 경우 전체 주문 목록을 보여줌
                showOrderList(orderColumnNames, selectedDate);
            }
        }
        public string[] setProductColumn()
        {
            string[] productColumnNames = { "", "상품코드", "상품명", "규격", "단가", "재고수량", "공급업체", "거래처", "판매", "품절" };
            return productColumnNames;
        }
        // 상품명으로 검색
        private void product_Name_textBox_TextChanged(object sender, EventArgs e)
        {
            // 반복 시 보여지는 목록 리스트 열 헤더

            // 검색어가 입력되지 않았을 경우 검색을 수행하지 않는다.
            if (!string.IsNullOrEmpty(product_Name_textBox.Text))
            {
                showOrderList(setProductColumn(), selectedDate);
            }
            else
            {
                // 검색어가 비어있을 경우 전체 주문 목록을 보여줌
                showOrderList(orderColumnNames, selectedDate);
            }
        }
        // 표시할 재고량 Min
        private void displayed_StockMin_textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(displayed_StockMin_textBox.Text) || int.Parse(displayed_StockMin_textBox.Text) >= int.Parse(displayed_StockMax_textBox.Text))
                {
                    MessageBox.Show("비어 있거나 최대 표시량 보다 값이 큽니다. 수량을 확인해 주세요.");
                    // 이전에 입력되어 있던 값으로 다시 설정
                    displayed_StockMin_textBox.Text = condition.getDisplayedStockMin().ToString();
                }
                else
                {
                    orderSystem.regConditionOrder(readCondition()); // 조건 저장
                    showOrderList(orderColumnNames, selectedDate); // 표시
                }
            }
        }
        // 표시할 재고량 Max
        private void displayed_StockMax_textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(displayed_StockMax_textBox.Text))
                {
                    MessageBox.Show("수량을 확인해 주세요.");
                    // 이전에 입력되어 있던 값으로 다시 설정
                    displayed_StockMax_textBox.Text = condition.getDisplayedStockMax().ToString();
                }
                else
                {
                    orderSystem.regConditionOrder(readCondition()); // 조건 저장
                    showOrderList(orderColumnNames, selectedDate); // 표시
                }
            }
        }
        // 표시되는 자동 발주될 수량 condtition
        private void auto_StockMin_textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(auto_StockMin_textBox.Text))
                {
                    MessageBox.Show("수량을 확인해 주세요.");
                    // 이전에 입력되어 있던 값으로 다시 설정
                    auto_StockMin_textBox.Text = condition.getAutoStockMin().ToString();
                }
                else
                {
                    orderSystem.regConditionOrder(readCondition()); // 조건 저장
                    showOrderList(orderColumnNames, selectedDate); // 표시
                }
            }
        }
        // 자동 발주할 수량 condition
        private void auto_Order_Quantity_textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(auto_Order_Quantity_textBox.Text))
                {
                    MessageBox.Show("수량을 확인해 주세요.");
                    // 이전에 입력되어 있던 값으로 다시 설정
                    auto_Order_Quantity_textBox.Text = condition.getStockQuantity().ToString();
                }
                else
                {
                    orderSystem.regConditionOrder(readCondition()); // 조건 저장
                    showOrderList(orderColumnNames, selectedDate); // 표시
                }
            }
        }
        private void order_Date_ValueChanged(object sender, EventArgs e)
        {
            // 날짜를 "yyyy-MM-dd" 형식의 string으로 변환
            selectedDate = order_Date.Value.ToString("yyyy-MM-dd");
            //showOrderList(orderColumnNames, selectedDate);
        }
    }
}
