using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testWinform
{
    internal class searchProductSystem
    {

        private ProductList productList;
        private Product[] productArray;
        private AutoOrderUI autoOrderUI;
        
        public searchProductSystem(AutoOrderUI autoOrderUI)
        {
            productList = new ProductList(); // ProductList 객체를 직접 생성
            productArray = productList.getProductArray();
            this.autoOrderUI = autoOrderUI; // 전달받은 AutoOrderUI 객체 사용
        }

        /// <검색기능>
        public void performSearch(string keyword, bool isCompany, string Date)
        {
            // 검색 결과를 저장할 임시 배열
            Product[] searchResult = new Product[5];

            // 검색어가 빈 문자열일 경우 전체 목록을 보여준다.
            if (string.IsNullOrEmpty(keyword))
            {
                searchResult = productArray;
            }
            else
            {
                if (isCompany) // 검색 대상이 거래처인 경우
                {
                    // 거래처 이름으로 검색
                    searchResult = productArray
                        .Where(x => x.getManufacturingCompany().Contains(keyword) || getInitial(x.getManufacturingCompany()).Contains(keyword))
                        .ToArray();
                }
                else // 검색 대상이 상품명인 경우
                {
                    // 상품명으로 검색
                    searchResult = productArray
                        .Where(x => x.getProductName().Contains(keyword) || getInitial(x.getProductName()).Contains(keyword))
                        .ToArray();
                }
            }
            // 검색 결과를 AutoOrderUI에 전달
            push_listview(searchResult, isCompany);
        }
        // 검색 결과 전달
        public void push_listview(Product[] productArray, bool isCompany)
        {
            ListViewItem totalListViewItem = new ListViewItem();
            ListViewItem[] productListViewItems = new ListViewItem[productArray.Length];
            int totalsupplyValue=0;
            int totalVAT = 0;
            int totalAmount = 0;
            int count = 0;
            // 거래처 검색 시
            if (isCompany)
            {
                // 반복하며 리스트에 추가
                foreach (var product in productArray)
                {
                    ListViewItem item = new ListViewItem("");
                    item.SubItems.Add(product.getManufacturingCompany());
                    item.SubItems.Add(product.getProductID().ToString());
                    item.SubItems.Add(product.getProductName());
                    item.SubItems.Add(product.getStandard());
                    item.SubItems.Add(product.getProductPrice().ToString());
                    item.SubItems.Add(product.getStockQuantity().ToString());
                    item.SubItems.Add(product.getSupplier());
                    item.SubItems.Add(product.getSell().ToString() == "True" ? "O" : "X");
                    item.SubItems.Add(product.getSoldOut().ToString() == "True" ? "O" : "X");
                    productListViewItems[count] = item;
                    count++;
                }
                totalListViewItem.SubItems.Add(totalsupplyValue.ToString());
                totalListViewItem.SubItems.Add(totalVAT.ToString());
                totalListViewItem.SubItems.Add(totalAmount.ToString());
                // ListView에 아이템 추가
                autoOrderUI.addOrderToListView(productListViewItems, totalListViewItem);
            }
            // 상품명 검색 시
            else
            {
                // 반복하며 리스트에 추가
                foreach (var product in productArray)
                {
                    ListViewItem item = new ListViewItem("");
                    item.SubItems.Add(product.getProductID().ToString());
                    item.SubItems.Add(product.getProductName());
                    item.SubItems.Add(product.getStandard());
                    item.SubItems.Add(product.getProductPrice().ToString());
                    item.SubItems.Add(product.getStockQuantity().ToString());
                    item.SubItems.Add(product.getSupplier());
                    item.SubItems.Add(product.getManufacturingCompany());
                    item.SubItems.Add(product.getSell().ToString() == "True" ? "O" : "X");
                    item.SubItems.Add(product.getSoldOut().ToString() == "True" ? "O" : "X");
                    productListViewItems[count] = item;
                    count++;
                }
                // ListView에 아이템 추가
                autoOrderUI.addOrderToListView(productListViewItems, totalListViewItem);
            }
        }
        private string getInitial(string text)
        {
            string chosungs = "ㄱㄲㄴㄷㄸㄹㅁㅂㅃㅅㅆㅇㅈㅉㅊㅋㅌㅍㅎ";
            StringBuilder result = new StringBuilder();

            foreach (char ch in text)
            {
                // 한글 일 경우
                if (ch >= '가' && ch <= '힣')
                {
                    int index = (ch - '가') / 588;
                    result.Append(chosungs[index]);
                }
                // 영문과 숫자일 경우 그대로 추가
                else if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch >= '0' && ch <= '9'))
                {
                    result.Append(ch);
                }
                // 그 외의 경우 무시
            }
            return result.ToString();
        }
    }
}
