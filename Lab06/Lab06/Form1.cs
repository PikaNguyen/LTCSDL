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

namespace Lab06
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void DisplayCategory(SqlDataReader reader)
        {
            lvCategory.Items.Clear();
            while(reader.Read())
            {
                ListViewItem item = new ListViewItem(reader["ID"].ToString());
                lvCategory.Items.Add(item);
                item.SubItems.Add(reader["Name"].ToString());
                item.SubItems.Add(reader["Type"].ToString());
            }
        }

        private void btnLoad_Click_1(object sender, EventArgs e)
        {
            string connectionString = "server=DESKTOP-PDOJ8VE; database = RestaurantManagement; Integrated Security = true;";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            string query = "SELECT ID, NAME, TYPE FROM Category";
            sqlCommand.CommandText = query;
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            this.DisplayCategory(sqlDataReader);
            sqlConnection.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string connectionString = "server=DESKTOP-PDOJ8VE; database = RestaurantManagement; Integrated Security = true;";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO Category(Name, [Type])" + "VALUES (N'" + txtCategoryName.Text + "', " + txtType.Text + ")";
            sqlConnection.Open();
            int numOfRowsEffected = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            if (numOfRowsEffected == 1)
            {
                MessageBox.Show("Thêm món ăn thành công !");
                btnLoad.PerformClick();
                txtID.Text = "";
                txtType.Text = "";
            }
            else
            {
                MessageBox.Show("Đã có lỗi xảy ra. Vui lòng thử lại !");
            }
        }

        private void lvCategory_Click(object sender, EventArgs e)
        {
            ListViewItem item = lvCategory.SelectedItems[0];
            txtID.Text = item.Text;
            txtCategoryName.Text = item.SubItems[1].Text;
            txtType.Text = item.SubItems[2].Text == "0" ? "Thức uống" : "Đồ ăn";
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string connectionString = "server = DESKTOP-PDOJ8VE; database = RestaurantManagement; Integrated Security = true;";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            string type = txtType.Text == "Thức ăn" ? "1" : "0";
            sqlCommand.CommandText = "UPDATE Category SET Name = N'"  + txtCategoryName.Text + 
                                            "', [Type] = "  + type +
                                            " WHERE ID = " + txtID.Text;
            sqlConnection.Open();
            int numOfRowsEffected = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            if (numOfRowsEffected == 1)
            {
                ListViewItem item = lvCategory.SelectedItems[0];
                item.SubItems[1].Text = txtCategoryName.Text;
                item.SubItems[2].Text = type ;
                txtID.Text = "";
                txtCategoryName.Text = "";
                txtType.Text = "";
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                MessageBox.Show("Cập nhật nhóm món ăn thành công !");
            }
            else
                MessageBox.Show("Lỗi !!!!");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string connectionString = "server = .; database = RestaurantManagement; Integrated Security = true;";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = "DELETE FROM Category " + "Where ID = " + txtID.Text;
            sqlConnection.Open();
            int numOfRowsEffected = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            if (numOfRowsEffected == 1)
            {
                ListViewItem item = lvCategory.SelectedItems[0];
                lvCategory.Items.Remove(item);
                 txtID.Text = "";
                 txtCategoryName.Text = "";
                txtType.Text = "";
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                MessageBox.Show("Xóa thành công");
            }
            else MessageBox.Show("Lỗi !!");
        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            if (lvCategory.SelectedItems.Count > 0) 
                btnDelete.PerformClick();
        }

        private void tsmViewFood_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                frmFood foodForm = new frmFood();
                foodForm.Show(this);
                foodForm.LoadFood(Convert.ToInt32(txtID.Text));
            }
        }

        private void btnBills_Click(object sender, EventArgs e)
        {
            frmBills bills = new frmBills();
            bills.Show(this);
           
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            frmAccountManager accountManager = new frmAccountManager();
            accountManager.Show(this);
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            frmTable table = new frmTable();
            table.Show(this);
        }
    }
}
