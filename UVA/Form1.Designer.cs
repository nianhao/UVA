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
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox_sysLog
            // 
            this.textBox_sysLog.Location = new System.Drawing.Point(761, 634);
            this.textBox_sysLog.Multiline = true;
            this.textBox_sysLog.Name = "textBox_sysLog";
            this.textBox_sysLog.ReadOnly = true;
            this.textBox_sysLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_sysLog.Size = new System.Drawing.Size(434, 160);
            this.textBox_sysLog.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.设置ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1233, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统设置ToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 系统设置ToolStripMenuItem
            // 
            this.系统设置ToolStripMenuItem.Name = "系统设置ToolStripMenuItem";
            this.系统设置ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.系统设置ToolStripMenuItem.Text = "系统设置";
            this.系统设置ToolStripMenuItem.Click += new System.EventHandler(this.系统设置ToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // comboBox_allUVA
            // 
            this.comboBox_allUVA.FormattingEnabled = true;
            this.comboBox_allUVA.Location = new System.Drawing.Point(761, 596);
            this.comboBox_allUVA.Name = "comboBox_allUVA";
            this.comboBox_allUVA.Size = new System.Drawing.Size(160, 20);
            this.comboBox_allUVA.TabIndex = 2;
            this.comboBox_allUVA.SelectedValueChanged += new System.EventHandler(this.comboBox_allUVA_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(759, 564);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "全部在线的无人机";
            // 
            // label_uvaNum
            // 
            this.label_uvaNum.AutoSize = true;
            this.label_uvaNum.Location = new System.Drawing.Point(876, 564);
            this.label_uvaNum.Name = "label_uvaNum";
            this.label_uvaNum.Size = new System.Drawing.Size(11, 12);
            this.label_uvaNum.TabIndex = 4;
            this.label_uvaNum.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(904, 563);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "台";
            // 
            // panel_VLCPlayer
            // 
            this.panel_VLCPlayer.BackColor = System.Drawing.Color.Gray;
            this.panel_VLCPlayer.Location = new System.Drawing.Point(34, 29);
            this.panel_VLCPlayer.Name = "panel_VLCPlayer";
            this.panel_VLCPlayer.Size = new System.Drawing.Size(701, 504);
            this.panel_VLCPlayer.TabIndex = 6;
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(319, 657);
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(233, 45);
            this.trackBar2.TabIndex = 13;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(45, 659);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 19);
            this.label2.TabIndex = 14;
            this.label2.Text = "带宽控制";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(190, 659);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 19);
            this.label4.TabIndex = 15;
            this.label4.Text = "M/100M";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numericUpDown1.ForeColor = System.Drawing.Color.Red;
            this.numericUpDown1.Location = new System.Drawing.Point(136, 657);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(47, 29);
            this.numericUpDown1.TabIndex = 16;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Red;
            this.panel1.Location = new System.Drawing.Point(761, 29);
            this.panel1.Margin = new System.Windows.Forms.Padding(35, 20, 20, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(216, 147);
            this.panel1.TabIndex = 20;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Orange;
            this.panel2.Location = new System.Drawing.Point(761, 203);
            this.panel2.Margin = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(216, 147);
            this.panel2.TabIndex = 21;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Yellow;
            this.panel3.Location = new System.Drawing.Point(761, 386);
            this.panel3.Margin = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(216, 147);
            this.panel3.TabIndex = 22;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel6.Location = new System.Drawing.Point(999, 386);
            this.panel6.Margin = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(216, 147);
            this.panel6.TabIndex = 25;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Green;
            this.panel4.Location = new System.Drawing.Point(999, 29);
            this.panel4.Margin = new System.Windows.Forms.Padding(35, 20, 20, 20);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(216, 147);
            this.panel4.TabIndex = 23;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Blue;
            this.panel5.Location = new System.Drawing.Point(999, 203);
            this.panel5.Margin = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(216, 147);
            this.panel5.TabIndex = 24;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1233, 822);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.textBox_sysLog);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.panel_VLCPlayer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label_uvaNum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_allUVA);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "无人机通信系统";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
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
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
    }
}

