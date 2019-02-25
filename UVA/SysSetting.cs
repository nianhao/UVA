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
        }

        private void button_confirm_Click(object sender, EventArgs e)
        {
            string ip = this.textBox_IP.Text.Trim();
            int port = Decimal.ToInt32(this.numericUpDown_port.Value);
            //这里应该验证是否符合
            Global.CONNECTION_IP = ip;
            Global.CONNECTION_PORT = port;
        }
    }
}
