namespace UVA
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_sysLog = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBox_allUVA = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label_uvaNum = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.serviceController1 = new System.ServiceProcess.ServiceController();
            this.panel_VLCPlayer = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.button_close = new System.Windows.Forms.Button();
            this.button_start = new System.Windows.Forms.Button();
            this.button_stop = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.buttonshowLinkInfo = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_sysLog
            // 
            this.textBox_sysLog.Location = new System.Drawing.Point(1015, 792);
            this.textBox_sysLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_sysLog.Multiline = true;
            this.textBox_sysLog.Name = "textBox_sysLog";
            this.textBox_sysLog.ReadOnly = true;
            this.textBox_sysLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_sysLog.Size = new System.Drawing.Size(577, 199);
            this.textBox_sysLog.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.设置ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1644, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统设置ToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 系统设置ToolStripMenuItem
            // 
            this.系统设置ToolStripMenuItem.Name = "系统设置ToolStripMenuItem";
            this.系统设置ToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.系统设置ToolStripMenuItem.Text = "系统设置";
            this.系统设置ToolStripMenuItem.Click += new System.EventHandler(this.系统设置ToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // comboBox_allUVA
            // 
            this.comboBox_allUVA.FormattingEnabled = true;
            this.comboBox_allUVA.Location = new System.Drawing.Point(1015, 745);
            this.comboBox_allUVA.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox_allUVA.Name = "comboBox_allUVA";
            this.comboBox_allUVA.Size = new System.Drawing.Size(212, 23);
            this.comboBox_allUVA.TabIndex = 2;
            this.comboBox_allUVA.SelectedValueChanged += new System.EventHandler(this.comboBox_allUVA_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1012, 705);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "全部在线的无人机";
            // 
            // label_uvaNum
            // 
            this.label_uvaNum.AutoSize = true;
            this.label_uvaNum.Location = new System.Drawing.Point(1168, 705);
            this.label_uvaNum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_uvaNum.Name = "label_uvaNum";
            this.label_uvaNum.Size = new System.Drawing.Size(15, 15);
            this.label_uvaNum.TabIndex = 4;
            this.label_uvaNum.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1205, 704);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "台";
            // 
            // panel_VLCPlayer
            // 
            this.panel_VLCPlayer.BackColor = System.Drawing.Color.Gray;
            this.panel_VLCPlayer.Location = new System.Drawing.Point(45, 36);
            this.panel_VLCPlayer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel_VLCPlayer.Name = "panel_VLCPlayer";
            this.panel_VLCPlayer.Size = new System.Drawing.Size(935, 630);
            this.panel_VLCPlayer.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(60, 824);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 24);
            this.label2.TabIndex = 14;
            this.label2.Text = "带宽控制";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(253, 824);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 24);
            this.label4.TabIndex = 15;
            this.label4.Text = "**M";
            //this.label4.Click += new System.EventHandler(this.Label4_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Red;
            this.panel1.Location = new System.Drawing.Point(1015, 36);
            this.panel1.Margin = new System.Windows.Forms.Padding(47, 25, 27, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(288, 184);
            this.panel1.TabIndex = 20;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Orange;
            this.panel2.Location = new System.Drawing.Point(1015, 254);
            this.panel2.Margin = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(288, 184);
            this.panel2.TabIndex = 21;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Yellow;
            this.panel3.Location = new System.Drawing.Point(1015, 482);
            this.panel3.Margin = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(288, 184);
            this.panel3.TabIndex = 22;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel6.Location = new System.Drawing.Point(1332, 482);
            this.panel6.Margin = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(288, 184);
            this.panel6.TabIndex = 25;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Green;
            this.panel4.Location = new System.Drawing.Point(1332, 36);
            this.panel4.Margin = new System.Windows.Forms.Padding(47, 25, 27, 25);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(288, 184);
            this.panel4.TabIndex = 23;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Blue;
            this.panel5.Location = new System.Drawing.Point(1332, 254);
            this.panel5.Margin = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(288, 184);
            this.panel5.TabIndex = 24;
            // 
            // button_close
            // 
            this.button_close.Location = new System.Drawing.Point(1285, 741);
            this.button_close.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(100, 29);
            this.button_close.TabIndex = 26;
            this.button_close.Text = "断开连接";
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(860, 792);
            this.button_start.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(100, 29);
            this.button_start.TabIndex = 27;
            this.button_start.Text = "开始监听";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // button_stop
            // 
            this.button_stop.Location = new System.Drawing.Point(860, 860);
            this.button_stop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(100, 29);
            this.button_stop.TabIndex = 28;
            this.button_stop.Text = "停止监听";
            this.button_stop.UseVisualStyleBackColor = true;
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(65, 926);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 29);
            this.button1.TabIndex = 29;
            this.button1.Text = "4";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(281, 926);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 29);
            this.button2.TabIndex = 30;
            this.button2.Text = "80";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(173, 926);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 29);
            this.button3.TabIndex = 31;
            this.button3.Text = "10";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(389, 926);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(100, 29);
            this.button4.TabIndex = 32;
            this.button4.Text = "200";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(497, 926);
            this.button5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(100, 29);
            this.button5.TabIndex = 33;
            this.button5.Text = "600";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // buttonshowLinkInfo
            // 
            this.buttonshowLinkInfo.Location = new System.Drawing.Point(429, 819);
            this.buttonshowLinkInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonshowLinkInfo.Name = "buttonshowLinkInfo";
            this.buttonshowLinkInfo.Size = new System.Drawing.Size(100, 29);
            this.buttonshowLinkInfo.TabIndex = 34;
            this.buttonshowLinkInfo.Text = "查看链路状态";
            this.buttonshowLinkInfo.UseVisualStyleBackColor = true;
            this.buttonshowLinkInfo.Click += new System.EventHandler(this.buttonshowLinkInfo_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1644, 1028);
            this.Controls.Add(this.buttonshowLinkInfo);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_stop);
            this.Controls.Add(this.button_start);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.textBox_sysLog);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label_uvaNum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_allUVA);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel_VLCPlayer);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "无人机通信系统";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_sysLog;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统设置ToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBox_allUVA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_uvaNum;
        private System.Windows.Forms.Label label3;
        private System.ServiceProcess.ServiceController serviceController1;
        private System.Windows.Forms.Panel panel_VLCPlayer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button button_close;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button buttonshowLinkInfo;
    }
}

