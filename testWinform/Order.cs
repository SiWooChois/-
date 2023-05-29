using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Forms;

namespace testWinform
{
    internal class Order
    {
        // 파일 경로
        private string filePathOrder = $"{DateTime.Now.ToString("yyyy-MM-dd")}.txt";  // 확장자 추가

        private int order_ID; // 주문번호
        private string productName; // 상품명
        private string standard; // 규격
        private int orderQuantity; // 수량
        private int productPrice; // 단가
        private int supplyValue; // 공급가액
        private int orderVAT; // 부가세
        private int totalValue; // 합계 금액
        private string orderDate; // 발주일자

        public Order(int order_ID, string productName, string standard, int orderQuantity, int productPrice,
                      int supplyValue, int orderVAT, int totalValue, string orderDate)
        {
            this.order_ID = order_ID;
            this.productName = productName;
            this.standard = standard;
            this.orderQuantity = orderQuantity;
            this.productPrice = productPrice;
            this.supplyValue = supplyValue;
            this.orderVAT = orderVAT;
            this.totalValue = totalValue;
            this.orderDate = orderDate;
        }

        public int getOrderID() { return order_ID; }
        public string getProductName() { return productName; }
        public string getStandard() { return standard; }
        public int getOrderQuantity() { return orderQuantity; }
        public int getProductPrice() { return productPrice; }
        public int getSupplyValue() { return supplyValue; }
        public int getOrderVAT() { return orderVAT; }
        public int getTotalValue() { return totalValue; }
        public string getOrderDate() { return orderDate; }
        public override string ToString()
        {
            return $"{order_ID},{productName},{standard},{orderQuantity},{productPrice},{supplyValue},{orderVAT},{totalValue},{orderDate}";
        }

        // 파일 읽어오기
        public Order[] getOrdersFile()
        {
            if (!File.Exists(filePathOrder))
            {
                return new Order[0]; // 파일이 없으면 빈 배열 반환
            }

            var lines = File.ReadAllLines(filePathOrder); // ReadAllLines 메서드는 string[]을 반환
            Order[] orders = new Order[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                var values = lines[i].Split(',');

                int order_ID = int.Parse(values[0]);
                string productName = values[1];
                string standard = values[2];
                int orderQuantity = int.Parse(values[3]);
                int productPrice = int.Parse(values[4]);
                int supplyValue = int.Parse(values[5]);
                int orderVAT = int.Parse(values[6]);
                int totalValue = int.Parse(values[7]);
                string orderDate = values[8];

                orders[i] = new Order(order_ID, productName, standard, orderQuantity, productPrice, supplyValue, orderVAT, totalValue, orderDate);
            }

            return orders;
        }

        public void addOrder(Order addOrder, int count)
        {
            Order[] orders = getOrdersFile();

            // Order 배열 확장
            Array.Resize(ref orders, orders.Length + 1);

            // 새 Order 추가
            orders[orders.Length - 1] = addOrder;
            if (count == 0)
            {
                // 새 Order를 계속 파일에 추가
                File.AppendAllText(filePathOrder, addOrder.ToString() + Environment.NewLine);
            }
            else
            {
                // 새 Order를 초기화
                File.WriteAllText(filePathOrder, addOrder.ToString() + Environment.NewLine);
            }
        }

        public bool delOrder(Order delOrder)
        {
            Order[] orders = getOrdersFile();
            int indexToDelete = Array.FindIndex(orders, o => o.order_ID == delOrder.order_ID);

            if (indexToDelete != -1)
            {
                orders = orders.Where((source, index) => index != indexToDelete).ToArray();
                File.WriteAllLines(filePathOrder, orders.Select(o => o.ToString()));
                return true; // 성공
            }
            else
            {
                return false; // 실패
            }
        }
    }
}
