using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2_Bai2
{
    public partial class TBGiaoVienForm : Form
    {
        public TBGiaoVienForm()
        {
            InitializeComponent();
        }
        public void SetText(string s)
        {
            lblThongBao.Text = s;
        }

    }
}
