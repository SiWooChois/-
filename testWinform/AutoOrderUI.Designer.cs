namespace testWinform
{
    partial class AutoOrderUI
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.displayed_StockMin_label = new System.Windows.Forms.Label();
            this.displayed_StockMax_label = new System.Windows.Forms.Label();
            this.auto_StockMin_label = new System.Windows.Forms.Label();
            this.auto_Order_Quantity_label = new System.Windows.Forms.Label();
            this.order_Date_label = new System.Windows.Forms.Label();
            this.manufacturing_company_label = new System.Windows.Forms.Label();
            this.product_Name_label = new System.Windows.Forms.Label();
            this.displayed_StockMin_textBox = new System.Windows.Forms.TextBox();
            this.displayed_StockMax_textBox = new System.Windows.Forms.TextBox();
            this.product_Name_textBox = new System.Windows.Forms.TextBox();
            this.auto_Order_Quantity_textBox = new System.Windows.Forms.TextBox();
            this.manufacturing_company_textBox = new System.Windows.Forms.TextBox();
            this.auto_StockMin_textBox = new System.Windows.Forms.TextBox();
            this.order_Date = new System.Windows.Forms.DateTimePicker();
            this.order_listView = new System.Windows.Forms.ListView();
            this.order_button = new System.Windows.Forms.Button();
            this.total_ListView = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // displayed_StockMin_label
            // 
            this.displayed_StockMin_label.AutoSize = true;
            this.displayed_StockMin_label.Location = new System.Drawing.Point(333, 17);
            this.displayed_StockMin_label.Name = "displayed_StockMin_label";
            this.displayed_StockMin_label.Size = new System.Drawing.Size(115, 12);
            this.displayed_StockMin_label.TabIndex = 0;
            this.displayed_StockMin_label.Text = "표시할 재고량(이상)";
            // 
            // displayed_StockMax_label
            // 
            this.displayed_StockMax_label.AutoSize = true;
            this.displayed_StockMax_label.Location = new System.Drawing.Point(579, 17);
            this.displayed_StockMax_label.Name = "displayed_StockMax_label";
            this.displayed_StockMax_label.Size = new System.Drawing.Size(115, 12);
            this.displayed_StockMax_label.TabIndex = 1;
            this.displayed_StockMax_label.Text = "표시할 재고량(미만)";
            // 
            // auto_StockMin_label
            // 
            this.auto_StockMin_label.AutoSize = true;
            this.auto_StockMin_label.Location = new System.Drawing.Point(333, 55);
            this.auto_StockMin_label.Name = "auto_StockMin_label";
            this.auto_StockMin_label.Size = new System.Drawing.Size(119, 12);
            this.auto_StockMin_label.TabIndex = 2;
            this.auto_StockMin_label.Text = "자동 발주 수량(미만)";
            // 
            // auto_Order_Quantity_label
            // 
            this.auto_Order_Quantity_label.AutoSize = true;
            this.auto_Order_Quantity_label.Location = new System.Drawing.Point(579, 55);
            this.auto_Order_Quantity_label.Name = "auto_Order_Quantity_label";
            this.auto_Order_Quantity_label.Size = new System.Drawing.Size(143, 12);
            this.auto_Order_Quantity_label.TabIndex = 3;
            this.auto_Order_Quantity_label.Text = "자동 발주될 수량(발주서)";
            // 
            // order_Date_label
            // 
            this.order_Date_label.AutoSize = true;
            this.order_Date_label.Location = new System.Drawing.Point(29, 18);
            this.order_Date_label.Name = "order_Date_label";
            this.order_Date_label.Size = new System.Drawing.Size(53, 12);
            this.order_Date_label.TabIndex = 4;
            this.order_Date_label.Text = "발주일자";
            // 
            // manufacturing_company_label
            // 
            this.manufacturing_company_label.AutoSize = true;
            this.manufacturing_company_label.Location = new System.Drawing.Point(29, 55);
            this.manufacturing_company_label.Name = "manufacturing_company_label";
            this.manufacturing_company_label.Size = new System.Drawing.Size(41, 12);
            this.manufacturing_company_label.TabIndex = 5;
            this.manufacturing_company_label.Text = "거래처";
            // 
            // product_Name_label
            // 
            this.product_Name_label.AutoSize = true;
            this.product_Name_label.Location = new System.Drawing.Point(29, 94);
            this.product_Name_label.Name = "product_Name_label";
            this.product_Name_label.Size = new System.Drawing.Size(41, 12);
            this.product_Name_label.TabIndex = 6;
            this.product_Name_label.Text = "상품명";
            // 
            // displayed_StockMin_textBox
            // 
            this.displayed_StockMin_textBox.Location = new System.Drawing.Point(458, 11);
            this.displayed_StockMin_textBox.Name = "displayed_StockMin_textBox";
            this.displayed_StockMin_textBox.Size = new System.Drawing.Size(100, 21);
            this.displayed_StockMin_textBox.TabIndex = 7;
            this.displayed_StockMin_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.displayed_StockMin_textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.displayed_StockMin_textBox_KeyDown);
            // 
            // displayed_StockMax_textBox
            // 
            this.displayed_StockMax_textBox.Location = new System.Drawing.Point(728, 14);
            this.displayed_StockMax_textBox.Name = "displayed_StockMax_textBox";
            this.displayed_StockMax_textBox.Size = new System.Drawing.Size(100, 21);
            this.displayed_StockMax_textBox.TabIndex = 8;
            this.displayed_StockMax_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.displayed_StockMax_textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.displayed_StockMax_textBox_KeyDown);
            // 
            // product_Name_textBox
            // 
            this.product_Name_textBox.Location = new System.Drawing.Point(93, 88);
            this.product_Name_textBox.Name = "product_Name_textBox";
            this.product_Name_textBox.Size = new System.Drawing.Size(200, 21);
            this.product_Name_textBox.TabIndex = 9;
            this.product_Name_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.product_Name_textBox.TextChanged += new System.EventHandler(this.product_Name_textBox_TextChanged);
            // 
            // auto_Order_Quantity_textBox
            // 
            this.auto_Order_Quantity_textBox.Location = new System.Drawing.Point(728, 52);
            this.auto_Order_Quantity_textBox.Name = "auto_Order_Quantity_textBox";
            this.auto_Order_Quantity_textBox.Size = new System.Drawing.Size(100, 21);
            this.auto_Order_Quantity_textBox.TabIndex = 10;
            this.auto_Order_Quantity_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.auto_Order_Quantity_textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.auto_Order_Quantity_textBox_KeyDown);
            // 
            // manufacturing_company_textBox
            // 
            this.manufacturing_company_textBox.Location = new System.Drawing.Point(93, 52);
            this.manufacturing_company_textBox.Name = "manufacturing_company_textBox";
            this.manufacturing_company_textBox.Size = new System.Drawing.Size(200, 21);
            this.manufacturing_company_textBox.TabIndex = 11;
            this.manufacturing_company_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.manufacturing_company_textBox.TextChanged += new System.EventHandler(this.manufacturing_company_textBox_TextChanged);
            // 
            // auto_StockMin_textBox
            // 
            this.auto_StockMin_textBox.Location = new System.Drawing.Point(458, 52);
            this.auto_StockMin_textBox.Name = "auto_StockMin_textBox";
            this.auto_StockMin_textBox.Size = new System.Drawing.Size(100, 21);
            this.auto_StockMin_textBox.TabIndex = 12;
            this.auto_StockMin_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.auto_StockMin_textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.auto_StockMin_textBox_KeyDown);
            // 
            // order_Date
            // 
            this.order_Date.Location = new System.Drawing.Point(93, 12);
            this.order_Date.Name = "order_Date";
            this.order_Date.Size = new System.Drawing.Size(200, 21);
            this.order_Date.TabIndex = 13;
            this.order_Date.ValueChanged += new System.EventHandler(this.order_Date_ValueChanged);
            // 
            // order_listView
            // 
            this.order_listView.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.order_listView.FullRowSelect = true;
            this.order_listView.GridLines = true;
            this.order_listView.HideSelection = false;
            this.order_listView.LabelEdit = true;
            this.order_listView.Location = new System.Drawing.Point(0, 126);
            this.order_listView.Name = "order_listView";
            this.order_listView.Size = new System.Drawing.Size(1097, 340);
            this.order_listView.TabIndex = 14;
            this.order_listView.UseCompatibleStateImageBehavior = false;
            this.order_listView.View = System.Windows.Forms.View.Details;
            // 
            // order_button
            // 
            this.order_button.Location = new System.Drawing.Point(959, 26);
            this.order_button.Name = "order_button";
            this.order_button.Size = new System.Drawing.Size(87, 59);
            this.order_button.TabIndex = 15;
            this.order_button.Text = "발주";
            this.order_button.UseVisualStyleBackColor = true;
            this.order_button.Click += new System.EventHandler(this.order_button_Click);
            // 
            // total_ListView
            // 
            this.total_ListView.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.total_ListView.ForeColor = System.Drawing.Color.Black;
            this.total_ListView.GridLines = true;
            this.total_ListView.HideSelection = false;
            this.total_ListView.Location = new System.Drawing.Point(0, 465);
            this.total_ListView.Name = "total_ListView";
            this.total_ListView.Size = new System.Drawing.Size(1097, 31);
            this.total_ListView.TabIndex = 16;
            this.total_ListView.UseCompatibleStateImageBehavior = false;
            this.total_ListView.View = System.Windows.Forms.View.Details;
            // 
            // AutoOrderUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1098, 498);
            this.Controls.Add(this.total_ListView);
            this.Controls.Add(this.order_button);
            this.Controls.Add(this.order_listView);
            this.Controls.Add(this.order_Date);
            this.Controls.Add(this.auto_StockMin_textBox);
            this.Controls.Add(this.manufacturing_company_textBox);
            this.Controls.Add(this.auto_Order_Quantity_textBox);
            this.Controls.Add(this.product_Name_textBox);
            this.Controls.Add(this.displayed_StockMax_textBox);
            this.Controls.Add(this.displayed_StockMin_textBox);
            this.Controls.Add(this.product_Name_label);
            this.Controls.Add(this.manufacturing_company_label);
            this.Controls.Add(this.order_Date_label);
            this.Controls.Add(this.auto_Order_Quantity_label);
            this.Controls.Add(this.auto_StockMin_label);
            this.Controls.Add(this.displayed_StockMax_label);
            this.Controls.Add(this.displayed_StockMin_label);
            this.Name = "AutoOrderUI";
            this.Text = "AutoOrderUI";
            this.Shown += new System.EventHandler(this.AutoOrderUI_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label displayed_StockMin_label;
        private System.Windows.Forms.Label displayed_StockMax_label;
        private System.Windows.Forms.Label auto_StockMin_label;
        private System.Windows.Forms.Label auto_Order_Quantity_label;
        private System.Windows.Forms.Label order_Date_label;
        private System.Windows.Forms.Label manufacturing_company_label;
        private System.Windows.Forms.Label product_Name_label;
        private System.Windows.Forms.TextBox displayed_StockMin_textBox;
        private System.Windows.Forms.TextBox displayed_StockMax_textBox;
        private System.Windows.Forms.TextBox product_Name_textBox;
        private System.Windows.Forms.TextBox auto_Order_Quantity_textBox;
        private System.Windows.Forms.TextBox manufacturing_company_textBox;
        private System.Windows.Forms.TextBox auto_StockMin_textBox;
        private System.Windows.Forms.DateTimePicker order_Date;
        private System.Windows.Forms.ListView order_listView;
        private System.Windows.Forms.Button order_button;
        private System.Windows.Forms.ListView total_ListView;
    }
}

