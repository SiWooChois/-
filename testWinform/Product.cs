using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace testWinform
{
    internal class Product
    {
        // jsonfile path
        private string filePathProduct = "Product.txt";

        private int product_ID; // 상품 고유 번호
        private string product_Name; // 상품명
        private string standard; // 규격
        private int product_Price; // 가격(단일)
        private int stock_Quantity; // 재고 수량
        private string supplier; // 공급업체
        private string manufacturing_Company; // 제조사
        private bool sell; // 판매여부 (아예 판매 x 삭제 대상)
        private bool sold_Out; // 품절여부 (재고 기다리는 중)
        private int product_Sale; // 할인가

        public Product(int product_ID, string product_Name, string standard, int product_Price, int stock_Quantity,
            string supplier, string manufacturing_Company, bool sell, bool sold_Out, int product_Sale)
        {
            this.product_ID = product_ID;
            this.product_Name = product_Name;
            this.standard = standard;
            this.product_Price = product_Price;
            this.stock_Quantity = stock_Quantity;
            this.supplier = supplier;
            this.manufacturing_Company = manufacturing_Company;
            this.sell = sell;
            this.sold_Out = sold_Out;
            this.product_Sale = product_Sale;
        }
        public int getProductID() { return product_ID; }
        public string getProductName() { return product_Name; }
        public string getStandard() { return standard; }
        public int getProductPrice() { return product_Price; }
        public int getStockQuantity() { return stock_Quantity; }
        public string getSupplier() { return supplier; }
        public string getManufacturingCompany() { return manufacturing_Company; }
        public bool getSell() { return sell; }
        public bool getSoldOut() { return sold_Out; }
        public int getProductSale() { return product_Sale; }

        // 파일 읽어오기
        public List<Product> getProductsFile()
        {
            var products = new List<Product>();

            if (File.Exists(filePathProduct))
            {
                var lines = File.ReadLines(filePathProduct);
                foreach (var line in lines)
                {
                    var values = line.Split(',');

                    int product_ID = int.Parse(values[0]);
                    string product_Name = values[1];
                    string standard = values[2];
                    int product_Price = int.Parse(values[3]);
                    int stock_Quantity = int.Parse(values[4]);
                    string supplier = values[5];
                    string manufacturing_Company = values[6];
                    bool sell = bool.Parse(values[7]);
                    bool sold_Out = bool.Parse(values[8]);
                    int product_Sale = int.Parse(values[9]);

                    products.Add(new Product(product_ID, product_Name, standard, product_Price, stock_Quantity,
                                             supplier, manufacturing_Company, sell, sold_Out, product_Sale));
                }
            }
            return products;
        }

        public void addProduct(Product addProduct)
        {
            var products = getProductsFile();
            products.Add(addProduct);

            File.AppendAllText(filePathProduct, addProduct.ToString());
        }

        public bool editProduct(Product editProduct)
        {
            var products = getProductsFile();
            var productToEdit = products.FirstOrDefault(p => p.product_ID == editProduct.product_ID);

            if (productToEdit != null)
            {
                // 상품 내용 변경
                productToEdit.product_ID = editProduct.product_ID;
                productToEdit.product_Name = editProduct.product_Name;
                productToEdit.standard = editProduct.standard;
                productToEdit.product_Price = editProduct.product_Price;
                productToEdit.stock_Quantity = editProduct.stock_Quantity;
                productToEdit.supplier = editProduct.supplier;
                productToEdit.manufacturing_Company = editProduct.manufacturing_Company;
                productToEdit.sell = editProduct.sell;
                productToEdit.sold_Out = editProduct.sold_Out;
                productToEdit.product_Sale = editProduct.product_Sale;

                // Updated products list write to text file
                File.WriteAllLines(filePathProduct, products.Select(p => p.ToString()));

                return true; // 성공
            }
            else
            {
                return false; // 실패
            }
        }

        public bool deleteProduct(Product delProduct)
        {
            var products = getProductsFile();
            var productToDelete = products.FirstOrDefault(p => p.product_ID == delProduct.product_ID);

            if (productToDelete != null)
            {
                products.Remove(productToDelete);

                // Updated products list write to text file
                File.WriteAllLines(filePathProduct, products.Select(p => p.ToString()));

                return true; // 성공
            }
            else
            {
                return false; // 실패
            }
        }
        // Tostring override to txtFile
        public override string ToString()
        {
            return $"{product_ID},{product_Name},{standard},{product_Price},{stock_Quantity},{supplier},{manufacturing_Company},{sell},{sold_Out},{product_Sale}";
        }
    }
}
