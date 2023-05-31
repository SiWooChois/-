using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace testWinform
{
    internal class OrderList
    {
        private Order[] order_Array;
        private string[] filePaths;

        public OrderList()
        {
            order_Array = new Order[0];
            filePaths = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.txt");
        }

        public Order[] getOrderArrayByDate(string Date)
        {
            string filePath = $"{Date}.txt";
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                order_Array = new Order[lines.Length];

                for (int i = 0; i < lines.Length; i++)
                {
                    var columns = lines[i].Split(',');
                    var order = new Order(
                        int.Parse(columns[0]),
                        columns[1],
                        columns[2],
                        int.Parse(columns[3]),
                        int.Parse(columns[4]),
                        int.Parse(columns[5]),
                        int.Parse(columns[6]),
                        int.Parse(columns[7]),
                        columns[8]
                    );
                    order_Array[i] = order;
                }
            }
            else
            {
                order_Array = new Order[0];
            }

            return order_Array;
        }

        // 전체 날짜 파일들에서 가져오기
        public Order[] getOrderArray()
        {
            List<Order> orderList = new List<Order>();

            var sortedFilePaths = filePaths.OrderByDescending(Path.GetFileNameWithoutExtension);
            foreach (var filePath in sortedFilePaths)
            {
                if (File.Exists(filePath))
                {
                    var lines = File.ReadAllLines(filePath);

                    for (int i = 0; i < lines.Length; i++)
                    {
                        var columns = lines[i].Split(',');

                        if (columns.Length == 9)
                        {
                            var order = new Order(
                                int.Parse(columns[0]),
                                columns[1],
                                columns[2],
                                int.Parse(columns[3]),
                                int.Parse(columns[4]),
                                int.Parse(columns[5]),
                                int.Parse(columns[6]),
                                int.Parse(columns[7]),
                                columns[8]
                            );
                            orderList.Add(order);
                        }
                    }
                }
            }
            return orderList.ToArray();
        }
    }
}