using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Globalization;

namespace testWinform
{
    internal class OrderSystem
    {
        private ProductList productList; // 물품 리스트 선언
        private OrderList orderList; // 발주 리스트 선언
        private AutoOrderUI autoOrderUI; // 생성자 생성 시 초기화
        private Product[] product_Array; // 상품 리스트 배열 선언
        private Order[] order_Array; // 발주 리스트 배열 선언
        private ConditionalOrder condition; // conditionalOrder 로 부터 조건 가져오기

        private string filePathOrder = $"{DateTime.Now.ToString("yyyy-MM-dd")}.txt";  // 확장자 추가
        public OrderSystem(AutoOrderUI autoOrderUI)
        {
            // 초기화
            this.autoOrderUI = autoOrderUI;
            productList = new ProductList();
            orderList = new OrderList();
            condition = new ConditionalOrder(0, 0, 0, 0);
            condition = condition.getConditional(); // 조건 가져오기
        }
        public ConditionalOrder getCondition()
        {
            return condition;
        }
        // 여기서 만들어진 OrderList(OrderList 생성 시 만들어짐)를 AutoOrderUI의 ListView의 Item으로 쓸 수 있게 해야 한다.
        public void showOrderItems(ConditionalOrder condition, string Date)
        {
            product_Array = productList.getProductArray(); // ProductList에서 product 배열 가져오기
            // 임시 변수 생성
            int totalsupplyValue = 0; // 총 공급가액
            int totalVAT = 0; // 총 부가세
            int totalAmount = 0; // 총 합계

            // 조건에 따른 발주 리스트 생성
            checkCodition(condition, Date);// 조건 검토
            ListViewItem[] orderListViewItems = new ListViewItem[product_Array.Length];
            for (int i = 0; i < product_Array.Length; i++)
            {
                int order_ID = i + 1; // 발주 넘버

                // AutoOrderUI 화면에 표시될 목록
                string productID = product_Array[i].getProductID().ToString();
                string productName = product_Array[i].getProductName();
                string manufacturingCompany = product_Array[i].getManufacturingCompany();
                string standard = product_Array[i].getStandard();
                string productQuantity = product_Array[i].getStockQuantity().ToString();
                string orderQuantity = condition.getStockQuantity().ToString();
                string productPrice = product_Array[i].getProductPrice().ToString();
                int supplyValuecalc = product_Array[i].getProductPrice() * condition.getStockQuantity();
                int orderVATcalc = supplyValuecalc / 10;
                int orderTotalAmountcalc = orderVATcalc + supplyValuecalc;
                string orderVAT = orderVATcalc.ToString();
                string supplyValue = supplyValuecalc.ToString();
                string orderTotalAmount = orderTotalAmountcalc.ToString();
                string orderDate = order_Array[i].getOrderDate(); // 발주 일자
                // 총 합계 계산
                totalsupplyValue += supplyValuecalc; // 총 공급가액
                totalVAT += orderVATcalc; // 총 부가세
                totalAmount += orderTotalAmountcalc; //총 합계금액

                //  orderListViewItem 객체를 생성, 첫 번째 열에 orderID를 설정
                ListViewItem orderListViewItem = new ListViewItem("");

                // orderListViewItem SubItems 컬렉션에 추가
                orderListViewItem.SubItems.Add(order_ID.ToString());
                orderListViewItem.SubItems.Add(productID);
                orderListViewItem.SubItems.Add(productName);
                orderListViewItem.SubItems.Add(manufacturingCompany);
                orderListViewItem.SubItems.Add(standard);
                orderListViewItem.SubItems.Add(productQuantity);
                orderListViewItem.SubItems.Add(orderQuantity);
                orderListViewItem.SubItems.Add(productPrice);
                orderListViewItem.SubItems.Add(supplyValue);
                orderListViewItem.SubItems.Add(orderVAT);
                orderListViewItem.SubItems.Add(orderTotalAmount);
                orderListViewItem.SubItems.Add(orderDate);

                orderListViewItems[i] = orderListViewItem;
            }
            ListViewItem totalListViewItem = new ListViewItem();
            totalListViewItem.SubItems.Add(totalsupplyValue.ToString());
            totalListViewItem.SubItems.Add(totalVAT.ToString());
            totalListViewItem.SubItems.Add(totalAmount.ToString());

            // AutoOrderUI 에 값 전달
            autoOrderUI.addOrderToListView(orderListViewItems, totalListViewItem);
        }

        // 조건 검토
        public void checkCodition(ConditionalOrder condition, string Date)
        {
            if (condition.getDisplayedStockMin() < condition.getDisplayedStockMax())
            {
                // ProductList에서 Product 배열 가져오기
                Product[] productArray = productList.getProductArray();
                Order[] orderArray = orderList.getOrderArray();

                // 조건에 맞는 상품의 수를 계산
                int count = 0;
                foreach (Product product in productArray)
                {
                    if (product.getStockQuantity() >= condition.getDisplayedStockMin() && product.getStockQuantity() <= condition.getDisplayedStockMax())
                    {
                        count++;
                    }
                }

                // Product 배열 초기화
                product_Array = new Product[count];
                order_Array = new Order[count];

                int orderIndex = 0;
                // Product 배열의 각 요소에 대해 Order 객체 생성
                foreach (Product product in productArray)
                {
                    if (product.getStockQuantity() >= condition.getDisplayedStockMin() && product.getStockQuantity() <= condition.getDisplayedStockMax())
                    {
                        // 조건에 맞는 것만 저장
                        product_Array[orderIndex] = product;

                        DateTime latestOrderDate = DateTime.MinValue;
                        foreach (Order order in orderArray)
                        {
                            // 모든 발주일자로 저장된 발주 목록 파일에 저장된 상품명과 product_Array의 상품명이 일치하는 경우 날짜를 저장
                            if (order.getProductName() == product.getProductName())
                            {
                                DateTime orderDate = DateTime.ParseExact(order.getOrderDate(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                                if (orderDate > latestOrderDate)
                                {
                                    latestOrderDate = orderDate;
                                    order_Array[orderIndex] = order;
                                }
                            }
                        }
                        orderIndex++;
                    }
                }
            }
            else
            {
                MessageBox.Show("표시할 재고량을 다시 확인해주세요");
            }
        }

        // 발주
        public bool Order(ConditionalOrder condition, Order[] orders, int check)
        {
            try
            {
                int count = 0;
                string order_Date = DateTime.Now.ToString("yyyy-MM-dd");
                string orderDetails = "";

                bool hasOrderProcessed = false;
                Order newOrder;
                Order[] newOrders = new Order[orders.Length];
                Order[] potentialOrders = new Order[orders.Length];
                int potentialOrdersCount = 0;

                for (int i = 0; i < orders.Length; i++)
                {
                    if (check == 0 && orders[i].getOrderQuantity() <= condition.getAutoStockMin())
                    {
                        orderDetails += $"상품명: {orders[i].getProductName()}, 발주 수량: {orders[i].getOrderQuantity()}, 합계: {orders[i].getTotalValue()}\n";
                        potentialOrders[potentialOrdersCount++] = orders[i];
                    }
                    else if (check == 1)  // 자동 발주가 아닌 경우 모든 상품을 발주 리스트에 추가
                    {
                        potentialOrders[potentialOrdersCount++] = orders[i];
                    }
                }

                if (check == 0 && orderDetails != "")
                {
                    DialogResult result = MessageBox.Show(orderDetails + "\n자동 발주하시겠습니까?", "자동 발주", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        // '아니오'를 눌렀을 때, 아무 것도 하지 않음
                        return false;
                    }
                }
                else if (check == 1)
                {
                    DialogResult result = MessageBox.Show("리스트에 있는 목록이 발주 됩니다. 발주 하시겠습니까?", "발주 확인", MessageBoxButtons.YesNo);

                    if (result == DialogResult.No)
                    {
                        return false;
                    }

                    if (File.Exists(filePathOrder))
                    {
                        // 메시지 표시
                        DialogResult dialogResult = MessageBox.Show("같은 날짜의 발주서가 존재합니다. 변경하시겠습니까?", "발주서 변경 확인", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            // 예
                            count = 1;
                        }
                        if (dialogResult == DialogResult.No)
                        {
                            // "아니요"를 선택한 경우
                            return false;
                        }
                    }
                }

                for (int i = 0; i < potentialOrdersCount; i++)
                {
                    Random rnd = new Random();
                    int RNo = rnd.Next(100000, 1000000);

                    newOrder = new Order(
                        RNo, // order_ID
                        potentialOrders[i].getProductName(), // 물품 명
                        potentialOrders[i].getStandard(), // 규격
                        potentialOrders[i].getOrderQuantity(), // 발주 수량
                        potentialOrders[i].getProductPrice(), // 물품 단가
                        potentialOrders[i].getSupplyValue(), // 공급가액
                        potentialOrders[i].getOrderVAT(), // 부가세
                        potentialOrders[i].getTotalValue(), // 합계
                        order_Date // 발주 날짜
                    );
                    newOrders[i] = newOrder;
                    // 발주서를 추가
                }
                newOrders[0].addOrder(newOrders, count); // addOrder를 여기에서 호출
                hasOrderProcessed = true;
                if (hasOrderProcessed)
                {
                    MessageBox.Show("발주가 완료되었습니다.");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("오류 발생" + e.ToString());
                return false;
            }
        }



        // 조건 수정
        public void regConditionOrder(ConditionalOrder condition)
        {
            condition.editConditional(condition);
        }

        // 현재 orderListView에 있는 물품 수정(상품명, 상품코드만)
        public void EditeOrderList(string productID)
        {
            // 물품 수정 코드
        }
        // 현재 orderListView에 있는 행 하나 삭제 (DeleteKey)
        public void DeleteorderList(string productID)
        {
            // 삭제 코드
        }
    }
}
