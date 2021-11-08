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
    public partial class frmBillsDetail : Form
    {
        public frmBillsDetail()
        {
            InitializeComponent();
        }

        string str = "server = .; database = RestaurantManagement; Integrated Security = true;";
        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        void LoadData()
        {
            command = connection.CreateCommand();
            command.CommandText = "SELECT * from BillDetails";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgvBillDetail.DataSource = table;
        }
        private void frmBillsDetail_Load(object sender, EventArgs e)
        {
            using (connection = new SqlConnection(str))
            {
                connection.Open();
                LoadData();
            }
        }
    }
}
