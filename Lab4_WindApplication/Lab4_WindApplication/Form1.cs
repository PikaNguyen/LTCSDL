using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4_WindApplication
{
    public partial class Form1 : Form
    {
        QuanLySinhVien qlsv;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Select Pictures";
            dlg.Multiselect = true;
            dlg.Filter = "Image Files (JPEG, GIF, BMP, etc.)|"
            + "*.jpg;*.jpeg;*.gif;*.bmp;"
            + "*.tif;*.tiff;*.png|"
            + "JPEG files (*.jpg;*.jpeg)|*.jpg;*.jpeg|"
            + "GIF files (*.gif)|*.gif|"
            + "BMP files (*.bmp)|*.bmp|"
            + "TIFF files (*.tif;*.tiff)|*.tif;*.tiff|"
            + "PNG files (*.png)|*.png|"
            + "All files (*.*)|*.*";
            dlg.InitialDirectory = Environment.CurrentDirectory;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var fileName = dlg.FileName;
                txtHinh.Text = fileName;
                pbHinh.Load(fileName);
            }
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            this.mtxtMaID.Text = "";
            this.txtHoTen.Text = "";
            this.txtEmail.Text = "";
            this.txtDiaChi.Text = "";
            this.dtpDateBirth.Value = DateTime.Now;
            this.cboClass.Text = this.cboClass.Items[0].ToString();
            this.mtxtSDT.Text = "";
            this.txtHinh.Text = "";
            this.pbHinh.ImageLocation = "";
            this.rdNam.Checked = true;
        }
        private void LoadListView()
        {
            this.lvSinhVien.Items.Clear();
            foreach (SinhVien sv in qlsv.DanhSach)
            {
                AddSV(sv);
            }

        }

        private void AddSV(SinhVien sv)
        {
            ListViewItem lvItem = new ListViewItem(sv.MaSo);
            lvItem.SubItems.Add(sv.HoTen);
            string gt = "Nữ";
            if (sv.Phai)
                gt = "Nam";
            lvItem.SubItems.Add(gt);
            lvItem.SubItems.Add(sv.NgaySinh.ToShortDateString());
            lvItem.SubItems.Add(sv.Lop);
            lvItem.SubItems.Add(sv.SDT);
            lvItem.SubItems.Add(sv.Email);
            lvItem.SubItems.Add(sv.DiaChi);     
            lvItem.SubItems.Add(sv.Hinh);
            lvSinhVien.Items.Add(lvItem);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            qlsv = new QuanLySinhVien();
            qlsv.DocTuFile();
            LoadListView();
        }
        public void InputInfo(SinhVien sv)
        {
            this.mtxtMaID.Text = sv.MaSo;
            this.txtHoTen.Text = sv.HoTen;
            this.txtEmail.Text = sv.Email;
            this.txtDiaChi.Text = sv.DiaChi;
            this.dtpDateBirth.Value = sv.NgaySinh;
            this.cboClass.Text = sv.Lop;
            this.mtxtSDT.Text = sv.SDT;
            this.pbHinh.ImageLocation = sv.Hinh;
            if (sv.Phai)
                this.rdNam.Checked = true;
            else
                this.rdNu.Checked = true;

        }
        private SinhVien GetSinhVien(ListViewItem lvItem)
        {
            SinhVien sv = new SinhVien();
            sv.MaSo = lvItem.SubItems[0].Text;
            sv.HoTen = lvItem.SubItems[1].Text;
            if (lvItem.SubItems[2].Text == "Nam")
                sv.Phai = true;
            sv.NgaySinh = DateTime.Parse(lvItem.SubItems[3].Text);
            sv.Lop = lvItem.SubItems[4].Text;
            sv.SDT = lvItem.SubItems[5].Text;
            sv.Email = lvItem.SubItems[6].Text;
            sv.DiaChi = lvItem.SubItems[7].Text;
            sv.Hinh = lvItem.SubItems[8].Text;
            return sv;
        }

        private void lvSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            int count = lvSinhVien.SelectedItems.Count;
            if (count > 0)
            {
                var lvItem = lvSinhVien.SelectedItems[0];         
                SinhVien sv = GetSinhVien(lvItem);
                InputInfo(sv);
            }
        }
        private int SoSanhTheoMa(object obj1, object obj2)
        {
            SinhVien sv = obj2 as SinhVien;
            return sv.MaSo.CompareTo(obj1);
        }
        private void btnSaveOrFix_Click(object sender, EventArgs e)
        {

            SinhVien sv = GetSinhVien();
            SinhVien find = qlsv.Tim(sv.MaSo, delegate (object obj1, object obj2)
            {
                return (obj2 as SinhVien).MaSo.CompareTo(obj1.ToString());
            });
            if (find != null)
            { bool kqsua;
                kqsua = qlsv.Sua(sv, sv.MaSo, SoSanhTheoMa);
                if (kqsua)
                {
                    this.LoadListView();
                }
            }
            else
            {
                this.qlsv.Add(sv);
                this.LoadListView();
            } 
                
            
        }

        private SinhVien GetSinhVien()
        {
           SinhVien sv = new SinhVien();
            bool gt = true;
           sv.MaSo = mtxtMaID.Text;
           sv.HoTen = txtHoTen.Text;
            sv.Email = txtEmail.Text;
            sv.DiaChi = txtDiaChi.Text;
            sv.NgaySinh = dtpDateBirth.Value;
            sv.Lop = cboClass.Text;
            sv.SDT = mtxtSDT.Text;
            sv.Hinh = txtHinh.Text;
            if (rdNu.Checked)
                gt = false;
            sv.Phai = gt;
            return sv;
        }

        private void tảiLạiDanhSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            qlsv = new QuanLySinhVien();
            qlsv.DocTuFile();
            LoadListView();
        }

        private void xóaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int count, i;
            ListViewItem lvItem;
            count = this.lvSinhVien.Items.Count - 1;
            for (i = count; i >= 0; i--)
            {
                lvItem = lvSinhVien.Items[i];
                if (lvItem.Checked)
                {
                    qlsv.Xoa(lvItem.SubItems[0].Text,SoSanhTheoMa);
                }
            }
            this.LoadListView();
            this.btnDefault.PerformClick();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {                       
            string fileN = "DSNV.txt";
            DialogResult result = MessageBox.Show("Có muốn lưu danh sách chỉnh sửa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result== DialogResult.Yes )
            {
                qlsv.GhiVaoFile(fileN);
               
            }
            else if (result == DialogResult.No)
            { 
                Application.Exit();
            }
        }
    }
}
