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
    public partial class OrderForm : Form
    {
        public OrderForm()
        {
            InitializeComponent();
        }
        string str = "server=.;database=RestaurantManagement;Integrated Security = true;";
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt = new DataTable();
        void LoadData()
        {
            conn = new SqlConnection(str);
            cmd = conn.CreateCommand();
            cmd.CommandText = "Select * from Bills";
            conn.Open();
            cmd.ExecuteNonQuery();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            dgvOrder.DataSource = dt;
            
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            
            LoadData();
            conn.Close();
            conn.Dispose();
        }

        private void dgvOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvOrder.Rows[e.RowIndex];
            string id = row.Cells[0].Value.ToString();
            OrderDetail frm = new OrderDetail();
            frm.Show(this);
            frm.LoadData(id);
        }

        private void dtpDateTime_ValueChanged(object sender, EventArgs e)
        {
            conn = new SqlConnection(str);
            cmd = conn.CreateCommand();
            cmd.CommandText = "Select * from Bills";
            conn.Open();
            cmd.ExecuteNonQuery();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            dgvOrder.DataSource = dt;
        }
    }
}
