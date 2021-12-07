using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogic;
using DataAccess;

namespace QuanLyThuVienCaNhan
{
    public partial class Form1 : Form
    {
        List<Category> listCategory = new List<Category>();
        List<Book> listBook = new List<Book>();
        int bookID = 0;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCategory();
        }
        private void CreatButton(int id, string name, int yPos)
		{
			var btn = new Button();
			btn.Text = name;
			btn.BackColor = Color.DarkOrchid;
			btn.Height = 50;
			btn.Width = 240;
			btn.ForeColor = Color.White;
			btn.Location = new Point(0, yPos);
			DanhSachTheLoai.Controls.Add(btn);
			btn.Tag = id;
            btn.Click += CategoryButton_Click;
		}

        private void CategoryButton_Click(object sender, EventArgs e)
        {
            BookBL bookBL = new BookBL();
            bookID = Convert.ToInt32((sender as Button).Tag);
            listBook = bookBL.GetAll();
            var books = listBook;
            if (bookID > 0)
            {
                books = books.Where(f => f.MaTheLoai == bookID).ToList();
            }
            LoadFoodToLvDetail(books);
        }
        private void LoadFoodToLvDetail(List<Book> list)
        {
            lsvDanhSach.Items.Clear();
            foreach (var book in list)
            {
                ListViewItem item = new ListViewItem(book.Ma.ToString());
                item.SubItems.Add(book.Name);
                item.SubItems.Add(book.MaTheLoai.ToString());
                item.SubItems.Add(book.TacGia);
                item.SubItems.Add(book.NhaXB);
                item.SubItems.Add(book.NamXB);
                item.SubItems.Add(book.KeSach.ToString());
                item.SubItems.Add(book.VTNgan.ToString());
                item.SubItems.Add(book.TrangThai.ToString());
                lsvDanhSach.Items.Add(item);
            }

        }
       
        private void LoadCategory()
		{
			CategoryBL category = new CategoryBL();
			listCategory = category.GetAll();
			var yPos = 0;
			CreatButton(0, "Tất cả", yPos);
			foreach (var cat in listCategory)
			{
				yPos += 45;
				CreatButton(cat.ID, cat.Name, yPos);
			}
		}
      
        private void tsmiMuonSach_Click(object sender, EventArgs e)
        {
            frmMuonTra frmMuon = new frmMuonTra();
            frmMuon.ShowDialog();
        }

        private void lsvDanhSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            frmSach frmSach = new frmSach();
            frmSach.ShowDialog();
        }

        private void tsmThongKe_Click(object sender, EventArgs e)
        {
            frmThongKe frmThongKe = new frmThongKe();
            frmThongKe.ShowDialog();
        }

        private void tsmThem_Click(object sender, EventArgs e)
        {
            frmSach frmSach = new frmSach();
            frmSach.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //test test = new test();
            //test.ShowDialog();
        }
    }
}
