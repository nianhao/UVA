using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UVA
{
    public partial class SysSetting : Form
    {
        public SysSetting()
        {
            InitializeComponent();
            this.textBox_IP.Text = Global.CONNECTION_IP;
            this.numericUpDown_port.Value = (Decimal)Global.CONNECTION_PORT;
            this.numericUpDown_MINPORT.Value = (Decimal)Global.MINPORT;
            this.numericUpDown_MAXPORT.Value = (Decimal)Global.MAXPORT;
            this.textBox_VIDEOPATH.Text = Global.videoSavePath;
            this.textBox_UPLOADURL.Text= Global.POSTION_POST_URL;
        }

        private void button_confirm_Click(object sender, EventArgs e)
        {
            string ip = this.textBox_IP.Text.Trim();
            int port = Decimal.ToInt32(this.numericUpDown_port.Value);
            //这里应该验证是否符合
            Global.CONNECTION_IP = ip;
            Global.CONNECTION_PORT = port;
            Global.MINPORT = Decimal.ToInt32(this.numericUpDown_MINPORT.Value);
            Global.MAXPORT = Decimal.ToInt32(this.numericUpDown_MAXPORT.Value);
            Global.videoSavePath= this.textBox_VIDEOPATH.Text;
            Global.POSTION_POST_URL= this.textBox_UPLOADURL.Text ;
        }
    }
}
