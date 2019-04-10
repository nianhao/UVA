namespace UVA
{
    partial class SysSetting
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_IP = new System.Windows.Forms.TextBox();
            this.numericUpDown_port = new System.Windows.Forms.NumericUpDown();
            this.button_confirm = new System.Windows.Forms.Button();
            this.button_cancle = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown_MINPORT = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_MAXPORT = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_VIDEOPATH = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_UPLOADURL = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_port)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MINPORT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MAXPORT)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "监听IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "监听端口";
            // 
            // textBox_IP
            // 
            this.textBox_IP.Location = new System.Drawing.Point(176, 28);
            this.textBox_IP.Name = "textBox_IP";
            this.textBox_IP.Size = new System.Drawing.Size(100, 21);
            this.textBox_IP.TabIndex = 2;
            // 
            // numericUpDown_port
            // 
            this.numericUpDown_port.Location = new System.Drawing.Point(176, 56);
            this.numericUpDown_port.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.numericUpDown_port.Minimum = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            this.numericUpDown_port.Name = "numericUpDown_port";
            this.numericUpDown_port.Size = new System.Drawing.Size(77, 21);
            this.numericUpDown_port.TabIndex = 3;
            this.numericUpDown_port.Value = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            // 
            // button_confirm
            // 
            this.button_confirm.Location = new System.Drawing.Point(134, 264);
            this.button_confirm.Name = "button_confirm";
            this.button_confirm.Size = new System.Drawing.Size(51, 39);
            this.button_confirm.TabIndex = 4;
            this.button_confirm.Text = "确认";
            this.button_confirm.UseVisualStyleBackColor = true;
            this.button_confirm.Click += new System.EventHandler(this.button_confirm_Click);
            // 
            // button_cancle
            // 
            this.button_cancle.Location = new System.Drawing.Point(225, 264);
            this.button_cancle.Name = "button_cancle";
            this.button_cancle.Size = new System.Drawing.Size(51, 39);
            this.button_cancle.TabIndex = 5;
            this.button_cancle.Text = "取消";
            this.button_cancle.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "视频接收端口范围";
            // 
            // numericUpDown_MINPORT
            // 
            this.numericUpDown_MINPORT.Location = new System.Drawing.Point(176, 85);
            this.numericUpDown_MINPORT.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.numericUpDown_MINPORT.Minimum = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            this.numericUpDown_MINPORT.Name = "numericUpDown_MINPORT";
            this.numericUpDown_MINPORT.Size = new System.Drawing.Size(55, 21);
            this.numericUpDown_MINPORT.TabIndex = 7;
            this.numericUpDown_MINPORT.Value = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            // 
            // numericUpDown_MAXPORT
            // 
            this.numericUpDown_MAXPORT.Location = new System.Drawing.Point(270, 85);
            this.numericUpDown_MAXPORT.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.numericUpDown_MAXPORT.Minimum = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            this.numericUpDown_MAXPORT.Name = "numericUpDown_MAXPORT";
            this.numericUpDown_MAXPORT.Size = new System.Drawing.Size(55, 21);
            this.numericUpDown_MAXPORT.TabIndex = 8;
            this.numericUpDown_MAXPORT.Value = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(240, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 22);
            this.label4.TabIndex = 9;
            this.label4.Text = "~";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "视频保存位置";
            // 
            // textBox_VIDEOPATH
            // 
            this.textBox_VIDEOPATH.Location = new System.Drawing.Point(176, 116);
            this.textBox_VIDEOPATH.Name = "textBox_VIDEOPATH";
            this.textBox_VIDEOPATH.Size = new System.Drawing.Size(149, 21);
            this.textBox_VIDEOPATH.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 146);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "GPS上传地址";
            // 
            // textBox_UPLOADURL
            // 
            this.textBox_UPLOADURL.Location = new System.Drawing.Point(176, 146);
            this.textBox_UPLOADURL.Name = "textBox_UPLOADURL";
            this.textBox_UPLOADURL.Size = new System.Drawing.Size(149, 21);
            this.textBox_UPLOADURL.TabIndex = 13;
            // 
            // SysSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 354);
            this.Controls.Add(this.textBox_UPLOADURL);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_VIDEOPATH);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDown_MAXPORT);
            this.Controls.Add(this.numericUpDown_MINPORT);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button_cancle);
            this.Controls.Add(this.button_confirm);
            this.Controls.Add(this.numericUpDown_port);
            this.Controls.Add(this.textBox_IP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "SysSetting";
            this.Text = "系统设置";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_port)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MINPORT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MAXPORT)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_IP;
        private System.Windows.Forms.NumericUpDown numericUpDown_port;
        private System.Windows.Forms.Button button_confirm;
        private System.Windows.Forms.Button button_cancle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown_MINPORT;
        private System.Windows.Forms.NumericUpDown numericUpDown_MAXPORT;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_VIDEOPATH;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_UPLOADURL;
    }
}