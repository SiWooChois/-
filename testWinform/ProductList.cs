using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using System.IO;

namespace testWinform
{
    internal class ProductList
    {
        private Product[] productArray; //배열 생성
        private string filePathProduct = "Product.txt"; // txt 파일 경로

        public ProductList()
        {
            // 생성자 메서드
            productArray = new Product[0]; // 초기에는 빈 배열로 초기화

        }
        // Product txt 파일을 읽어와서 반환
        public Product[] getProductArray()
        {
            if (File.Exists(filePathProduct))
            {
                var lines = File.ReadAllLines(filePathProduct);
                productArray = new Product[lines.Length];

                for (int i = 0; i < lines.Length; i++)
                {
                    var columns = lines[i].Split(',');
                    var product = new Product(
                        int.Parse(columns[0]),
                        columns[1],
                        columns[2],
                        int.Parse(columns[3]),
                        int.Parse(columns[4]),
                        columns[5],
                        columns[6],
                        bool.Parse(columns[7]),
                        bool.Parse(columns[8]),
                        int.Parse(columns[9])
                    );
                    productArray[i] = product;
                }
            }
            return productArray;
        }
    }
}
