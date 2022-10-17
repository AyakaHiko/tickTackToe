namespace server
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.connectConfig = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.portBox = new System.Windows.Forms.NumericUpDown();
            this.addressBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.startBtn = new System.Windows.Forms.Button();
            this.logBox = new System.Windows.Forms.ListBox();
            this.connectConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portBox)).BeginInit();
            this.SuspendLayout();
            // 
            // connectConfig
            // 
            this.connectConfig.Controls.Add(this.label2);
            this.connectConfig.Controls.Add(this.portBox);
            this.connectConfig.Controls.Add(this.addressBox);
            this.connectConfig.Controls.Add(this.label3);
            this.connectConfig.Location = new System.Drawing.Point(12, 12);
            this.connectConfig.Name = "connectConfig";
            this.connectConfig.Size = new System.Drawing.Size(334, 53);
            this.connectConfig.TabIndex = 7;
            this.connectConfig.TabStop = false;
            this.connectConfig.Text = "Connection";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(212, 24);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Port";
            // 
            // portBox
            // 
            this.portBox.Location = new System.Drawing.Point(249, 21);
            this.portBox.Margin = new System.Windows.Forms.Padding(2);
            this.portBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.portBox.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.portBox.Name = "portBox";
            this.portBox.Size = new System.Drawing.Size(65, 20);
            this.portBox.TabIndex = 5;
            this.portBox.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // addressBox
            // 
            this.addressBox.Location = new System.Drawing.Point(82, 23);
            this.addressBox.Name = "addressBox";
            this.addressBox.Size = new System.Drawing.Size(125, 20);
            this.addressBox.TabIndex = 2;
            this.addressBox.Text = "127.0.0.1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "IP Address";
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(13, 71);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 23);
            this.startBtn.TabIndex = 8;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // logBox
            // 
            this.logBox.FormattingEnabled = true;
            this.logBox.Location = new System.Drawing.Point(13, 100);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(349, 134);
            this.logBox.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 240);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.connectConfig);
            this.Name = "Form1";
            this.Text = "Form1";
            this.connectConfig.ResumeLayout(false);
            this.connectConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox connectConfig;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown portBox;
        private System.Windows.Forms.TextBox addressBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.ListBox logBox;
    }
}

