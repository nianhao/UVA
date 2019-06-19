namespace UVA
{
    partial class linkInfo
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
            this.labelTips = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelTips
            // 
            this.labelTips.AutoSize = true;
            this.labelTips.Location = new System.Drawing.Point(353, 25);
            this.labelTips.Name = "labelTips";
            this.labelTips.Size = new System.Drawing.Size(0, 12);
            this.labelTips.TabIndex = 0;
            this.labelTips.SizeChanged += new System.EventHandler(this.linkInfo_SizeChanged);
            this.labelTips.MouseDown += new System.Windows.Forms.MouseEventHandler(this.linkInfo_MouseDown);
            // 
            // linkInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelTips);
            this.Name = "linkInfo";
            this.Text = "链路信息";
            this.SizeChanged += new System.EventHandler(this.linkInfo_SizeChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.linkInfo_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTips;
    }
}