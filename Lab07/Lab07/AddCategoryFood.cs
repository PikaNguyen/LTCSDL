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

namespace Lab07
{
    public partial class AddCategoryFood : Form
    {
        public AddCategoryFood()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string connectionString = "server=.; database = RestaurantManagement; Integrated Security = true;";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO Category(Name, [Type])" + "VALUES (N'" + txtName.Text + "', " + txtType.Text + ")";
            connection.Open();
            int numOfRowsEffected = sqlCommand.ExecuteNonQuery();
            connection.Close();
            if (numOfRowsEffected == 1)
            {
                MessageBox.Show("Thêm nhóm món ăn thành công !","Thông báo!!!");
                txtName.Text = "";
                txtType.Text = "";
            }
            else
            {
                MessageBox.Show("Đã có lỗi xảy ra. Vui lòng thử lại !");
            }
            connection.Dispose();
            this.DialogResult = DialogResult.OK;
        }

    }
}
