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
    public partial class FindIDGVForm : Form

    {
        public QuanLyGiaoVien qlGiaoVien;
        public FindIDGVForm()
        {
            InitializeComponent();
        }

        private void rdMaGV_CheckedChanged(object sender, EventArgs e)
        {
            if (rdMaGV.Checked)
            {
                lbTimTheo.Text = rdMaGV.Text;
                txtTimKiem.Text = "";
            }
        }

        private void rdHoTen_CheckedChanged(object sender, EventArgs e)
        {
            if (rdHoTen.Checked)
            {
                lbTimTheo.Text = rdHoTen.Text;
                txtTimKiem.Text = "";
            }
        }

        private void rdSDT_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSDT.Checked)
            {
                lbTimTheo.Text = rdSDT.Text;
                txtTimKiem.Text = "";
            }
        }
        public FindIDGVForm(QuanLyGiaoVien qlGV) 
        {
            qlGiaoVien = qlGV;
        }

        private void btnOke_Click(object sender, EventArgs e)
        {
            var kieu = KieuTim.TheoHoTen;
            if (rdMaGV.Checked)
            {
                kieu = KieuTim.TheoMa;
            }
            else if (rdHoTen.Checked)
            {
                kieu = KieuTim.TheoHoTen;
            }
            else if (rdSDT.Checked)
            {
                kieu = KieuTim.TheoSDT;
            }

            
                MessageBox.Show("Không tìm thấy thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
            
        }
    }
}
