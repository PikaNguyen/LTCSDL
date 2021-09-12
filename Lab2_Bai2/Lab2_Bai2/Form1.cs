using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2_Bai2
{
    public partial class GiaoVienFrom : Form
    {
        private QuanLyGiaoVien qlGiaoVien;
        public GiaoVienFrom()
        {
            qlGiaoVien = new QuanLyGiaoVien();
            InitializeComponent();
        }
       
        private void TBGiaoVienFrom_Load(object sender, EventArgs e)
        {
            string lienhe = "http://it.dlu.edu.vn/e-learning/Default.aspx";
            linklbLienHe.Links.Add(0, lienhe.Length, lienhe);
            cboMaID.SelectedItem = cboMaID.Items[0];
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            int i = lbDanhSach.SelectedItems.Count - 1;
            while (i >= 0)
            {
                lbMonHoc.Items.Add(lbDanhSach.SelectedItems[i]);
                lbDanhSach.Items.Remove(lbDanhSach.SelectedItems[i]);
                i--;
            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int i = lbMonHoc.SelectedItems.Count - 1;
            while (i >= 0)
            {
                lbDanhSach.Items.Add(lbMonHoc.SelectedItems[i]);
                lbMonHoc.Items.Remove(lbMonHoc.SelectedItems[i]);
               
                i--;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void Reset()
        {
            cboMaID.Text = "";
            txtHoTen.Text = "";
            txtMail.Text = "";
            mtxtSDT.Text = "";
            rdNam.Checked = true;
           for(int i=0;i< chklbNgoaiNgu.Items.Count - 1; i++)
            {
                chklbNgoaiNgu.SetItemChecked(i, false);
            }
            foreach (object ob in lbMonHoc.Items)
                lbDanhSach.Items.Add(ob);
            lbMonHoc.Items.Clear();
        }

        private void linklbLienHe_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string s = e.Link.LinkData.ToString();
            Process.Start(s);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            TBGiaoVienForm frm = new TBGiaoVienForm();
            frm.SetText(GetGiaoVien().ToString());
            frm.ShowDialog();
        }

        private GiaoVien GetGiaoVien()
        {
            string gt = "Nam";
            if (rdNu.Checked)
                gt = "Nữ";
            GiaoVien gv = new GiaoVien();
            gv.MaSo = cboMaID.Text;
            gv.GioiTinh = gt;
            gv.HoTen = txtHoTen.Text;
            gv.NgaySinh = dtpDateBirth.Value;
            gv.Mail = txtMail.Text;
            gv.SDT = mtxtSDT.Text;
            string ngoaiNgu = "";
            for (int i = 0; i < chklbNgoaiNgu.Items.Count - 1; i++)
                if (chklbNgoaiNgu.GetItemChecked(i))
                    ngoaiNgu += chklbNgoaiNgu.Items[i] + ";";
            gv.NgoaiNgu = ngoaiNgu.Split(';');
            DanhMucMonHoc mh = new DanhMucMonHoc();
            foreach (var a in lbMonHoc.Items)
                mh.Them(new MonHoc(a.ToString()));
            gv.dsMonHoc = mh;
            return gv;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var giaoVien = GetGiaoVien();

            var success = qlGiaoVien.Them(giaoVien);
            if (!success)
                MessageBox.Show("Giáo viên mã số: " + giaoVien.MaSo + " đã tồn tại!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("Thêm giáo viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            var FindForm = new FindIDGVForm();
            FindForm.ShowDialog();
        }
    }
}
