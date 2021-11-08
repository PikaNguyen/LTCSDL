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
    public partial class frmRole : Form
    {
        public frmRole()
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
            command.CommandText = "SELECT * from Role";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgvRole.DataSource = table;
        }
        public void LoadRole(string accountN )
        {
            connection = new SqlConnection(str);
            command = connection.CreateCommand();
            command.CommandText = "SELECT a.AccountName, r.RoleName " +
                 " from Role r, RoleAccount ra, Account a" +
                 " Where a.AccountName = ra.AccountName and r.ID = ra.RoleID and a.AccountName = '" + accountN + "'";

            connection.Open();

            this.Text = "Vai trò tài khoản:  " + accountN;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable("Role");
            adapter.Fill(table);

            dgvRole.DataSource = table;
            dgvRole.Columns[0].ReadOnly = true;
            dgvRole.Columns[0].HeaderText = "Tài khoản";
            dgvRole.Columns[1].HeaderText = "Quyền";
            connection.Close();
        }
        private void frmRole_Load(object sender, EventArgs e)
        {
           // connection = new SqlConnection(str);
           // connection.Open();
           // LoadData();

        }
    }
}
