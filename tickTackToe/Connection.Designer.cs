namespace tickTackToe
{
    partial class Connection
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
            this.connectBtn = new System.Windows.Forms.Button();
            this.informLbl = new System.Windows.Forms.Label();
            this.connectConfig = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.portBox = new System.Windows.Forms.NumericUpDown();
            this.addressBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.connectConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portBox)).BeginInit();
            this.SuspendLayout();
            // 
            // connectBtn
            // 
            this.connectBtn.Location = new System.Drawing.Point(106, 85);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(144, 23);
            this.connectBtn.TabIndex = 0;
            this.connectBtn.Text = "Connect";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // informLbl
            // 
            this.informLbl.AutoSize = true;
            this.informLbl.Location = new System.Drawing.Point(154, 136);
            this.informLbl.Name = "informLbl";
            this.informLbl.Size = new System.Drawing.Size(43, 13);
            this.informLbl.TabIndex = 1;
            this.informLbl.Text = "Waiting";
            this.informLbl.Visible = false;
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
            this.connectConfig.TabIndex = 6;
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
            // Connection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 173);
            this.Controls.Add(this.connectConfig);
            this.Controls.Add(this.informLbl);
            this.Controls.Add(this.connectBtn);
            this.Name = "Connection";
            this.Text = "Connection";
            this.Load += new System.EventHandler(this.Connection_Load);
            this.connectConfig.ResumeLayout(false);
            this.connectConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.Label informLbl;
        private System.Windows.Forms.GroupBox connectConfig;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown portBox;
        private System.Windows.Forms.TextBox addressBox;
        private System.Windows.Forms.Label label3;
    }
}