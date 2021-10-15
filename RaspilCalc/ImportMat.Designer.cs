namespace RaspilCalc
{
    partial class ImportMat
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cbSize = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbManufactor = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbZ = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbArticle = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbPrice = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbPriceM2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lbConsole = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // cbSize
            // 
            this.cbSize.FormattingEnabled = true;
            this.errorProvider.SetIconAlignment(this.cbSize, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.cbSize.Location = new System.Drawing.Point(26, 78);
            this.cbSize.Name = "cbSize";
            this.cbSize.Size = new System.Drawing.Size(121, 21);
            this.cbSize.TabIndex = 3;
            this.toolTip1.SetToolTip(this.cbSize, "Размер листа в мм, например 2800x2070");
            this.cbSize.SelectedIndexChanged += new System.EventHandler(this.cbSize_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(156, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Размеры листа";
            this.toolTip1.SetToolTip(this.label1, "Размер листа в мм, например 2800x2070");
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // cbManufactor
            // 
            this.cbManufactor.FormattingEnabled = true;
            this.errorProvider.SetIconAlignment(this.cbManufactor, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.cbManufactor.Location = new System.Drawing.Point(26, 145);
            this.cbManufactor.Name = "cbManufactor";
            this.cbManufactor.Size = new System.Drawing.Size(121, 21);
            this.cbManufactor.TabIndex = 5;
            this.toolTip1.SetToolTip(this.cbManufactor, "Название производителя материала");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(156, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Производитель";
            this.toolTip1.SetToolTip(this.label2, "Название производителя материала");
            // 
            // tbZ
            // 
            this.errorProvider.SetIconAlignment(this.tbZ, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.tbZ.Location = new System.Drawing.Point(26, 112);
            this.tbZ.Name = "tbZ";
            this.tbZ.Size = new System.Drawing.Size(121, 20);
            this.tbZ.TabIndex = 4;
            this.toolTip1.SetToolTip(this.tbZ, "Толщина листа в милиметрах");
            this.tbZ.TextChanged += new System.EventHandler(this.tbZ_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(156, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Толщина";
            this.toolTip1.SetToolTip(this.label3, "Толщина листа в милиметрах");
            // 
            // tbName
            // 
            this.errorProvider.SetIconAlignment(this.tbName, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.tbName.Location = new System.Drawing.Point(27, 12);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(121, 20);
            this.tbName.TabIndex = 1;
            this.toolTip1.SetToolTip(this.tbName, "Названиее материала");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(156, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Название";
            this.toolTip1.SetToolTip(this.label4, "Названиее материала");
            this.label4.Click += new System.EventHandler(this.label1_Click);
            // 
            // tbArticle
            // 
            this.errorProvider.SetIconAlignment(this.tbArticle, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.tbArticle.Location = new System.Drawing.Point(27, 45);
            this.tbArticle.Name = "tbArticle";
            this.tbArticle.Size = new System.Drawing.Size(121, 20);
            this.tbArticle.TabIndex = 2;
            this.toolTip1.SetToolTip(this.tbArticle, "Кодовое обозначение материала у производителя");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(156, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Код( Артикл)";
            this.toolTip1.SetToolTip(this.label5, "Кодовое обозначение материала у производителя");
            this.label5.Click += new System.EventHandler(this.label1_Click);
            // 
            // tbPrice
            // 
            this.tbPrice.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.errorProvider.SetIconAlignment(this.tbPrice, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.tbPrice.Location = new System.Drawing.Point(26, 179);
            this.tbPrice.Name = "tbPrice";
            this.tbPrice.Size = new System.Drawing.Size(121, 23);
            this.tbPrice.TabIndex = 6;
            this.toolTip1.SetToolTip(this.tbPrice, "Цена одного целого  листа. руб\r\nОбязятельный параметр.");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(156, 182);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Цена за 1 лист";
            this.toolTip1.SetToolTip(this.label6, "Цена одного целого  листа. руб\r\nОбязятельный параметр.");
            this.label6.Click += new System.EventHandler(this.label1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(154, 215);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "*Цена за 1 м2";
            this.toolTip1.SetToolTip(this.label7, "Цена материала за 1 квадратный метр.\r\nЕсли оставить поле пустым, то автоматически" +
        " \r\nбудет посчитана стоимость исходя из площади \r\nи цены за 1 лист. ");
            this.label7.Click += new System.EventHandler(this.label1_Click);
            // 
            // tbPriceM2
            // 
            this.tbPriceM2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.errorProvider.SetIconAlignment(this.tbPriceM2, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.tbPriceM2.Location = new System.Drawing.Point(26, 212);
            this.tbPriceM2.Name = "tbPriceM2";
            this.tbPriceM2.Size = new System.Drawing.Size(121, 23);
            this.tbPriceM2.TabIndex = 7;
            this.toolTip1.SetToolTip(this.tbPriceM2, "Цена материала за 1 квадратный метр.\r\nЕсли оставить поле пустым, то автоматически" +
        " \r\nбудет посчитана стоимость исходя из площади \r\nи цены за 1 лист. ");
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Green;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(25, 252);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 48);
            this.button1.TabIndex = 8;
            this.button1.Text = "Добавить";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.toolTip1.ForeColor = System.Drawing.Color.Black;
            this.toolTip1.InitialDelay = 300;
            this.toolTip1.ReshowDelay = 100;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // lbConsole
            // 
            this.lbConsole.AutoSize = true;
            this.lbConsole.Location = new System.Drawing.Point(156, 258);
            this.lbConsole.Name = "lbConsole";
            this.lbConsole.Size = new System.Drawing.Size(0, 13);
            this.lbConsole.TabIndex = 9;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // ImportMat
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 334);
            this.Controls.Add(this.lbConsole);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbPriceM2);
            this.Controls.Add(this.tbPrice);
            this.Controls.Add(this.tbArticle);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.tbZ);
            this.Controls.Add(this.cbManufactor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbSize);
            this.Name = "ImportMat";
            this.Text = "Новый материал";
            this.Load += new System.EventHandler(this.ImportMat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbManufactor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbZ;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbArticle;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbPrice;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbPriceM2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lbConsole;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}