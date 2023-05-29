using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace testWinform
{
    internal class ConditionalOrder
    {
        private string filePathCondition = "Condition.txt";

        private int displayedStockMin;  // 표시할 최소 재고량
        private int displayedStockMax; //표시할 최대 재고량
        private int autoStockMin; // 자동 발주 표시될 재고량
        private int stockQuantity; // 자동 발주될 재고량


        public ConditionalOrder(int displayedStockMin, int displayedStockMax, int autoStockMin, int stockQuantity)
        {
            this.displayedStockMin = displayedStockMin;
            this.displayedStockMax = displayedStockMax;
            this.autoStockMin = autoStockMin;
            this.stockQuantity = stockQuantity;
        }

        public int getStockQuantity() { return stockQuantity; }
        public int getAutoStockMin() { return autoStockMin; }
        public int getDisplayedStockMin() { return displayedStockMin; }
        public int getDisplayedStockMax() { return displayedStockMax; }

        public override string ToString()
        {
            return $"{stockQuantity},{autoStockMin},{displayedStockMin},{displayedStockMax}";
        }
        // 조건 가져오기
        public ConditionalOrder getConditional()
        {
            if (File.Exists(filePathCondition))
            {
                var line = File.ReadLines(filePathCondition).FirstOrDefault();
                if (line != null)
                {
                    var values = line.Split(',');

                    int displayedStockMin = int.Parse(values[2]);
                    int displayedStockMax = int.Parse(values[3]);
                    int stockQuantity = int.Parse(values[0]);
                    int autoStockMin = int.Parse(values[1]);
                    
                    return new ConditionalOrder(displayedStockMin, displayedStockMax, autoStockMin, stockQuantity);
                }
            }
            return null; // 파일이 없거나 빈 경우 null 반환
        }
        // 조건 수정
        public void editConditional(ConditionalOrder editConditional)
        {
            // 새로운 조건으로 파일 덮어쓰기
            File.WriteAllText(filePathCondition, editConditional.ToString());
        }
    }
}
