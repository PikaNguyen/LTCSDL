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
    public partial class OrderDetail : Form
    {
        public OrderDetail()
        {
            InitializeComponent();
        }
        string str = "server=.;database=RestaurantManagement;Integrated Security = true;";
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt = new DataTable();
       public void LoadData(string ID)
        {
            conn = new SqlConnection(str);
            cmd = conn.CreateCommand();
            cmd.CommandText = "select A.[ID],B.[Name] as 'Tên món ăn',B.[Price] as 'Đơn giá',[Quantity] as 'Số lượng',B.Price*Quantity as 'Thành tiền' " +
                     "from BillDetails A,Food B where A.FoodID = B.ID and[InvoiceID] = " + ID;
            conn.Open();
            cmd.ExecuteNonQuery();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            dgvDetails.DataSource = dt;
            conn.Close();
            conn.Dispose();

        }

    }
}
