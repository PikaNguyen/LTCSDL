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
    public partial class frmBills : Form
    {
        string str = "server = .; database = RestaurantManagement; Integrated Security = true;";
        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        public frmBills()
        {
            InitializeComponent();
        }
        void LoadData()
        {
            command = connection.CreateCommand();
            command.CommandText = "SELECT * from Bills";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgvBills.DataSource = table;
        }

        private void BillsForm_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            LoadData();
        }

        private void dgvBills_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            frmBillsDetail billsDetail = new frmBillsDetail();
            billsDetail.Show();
        }
    }
}
