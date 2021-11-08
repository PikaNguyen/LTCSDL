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
    public partial class frmAccountManager : Form
    {
        string str = "server = .; database = RestaurantManagement; Integrated Security = true;";
        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        public frmAccountManager()
        {
            InitializeComponent();
        }
        void LoadData()
        {
            command = connection.CreateCommand();
            command.CommandText = "SELECT * from Account";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgvAccountMa.DataSource = table;
        }
        private void AccountManager_Load(object sender, EventArgs e)
        {

        }

        private void btnGroup_Click(object sender, EventArgs e)
        {
            using (connection = new SqlConnection(str))
            {
                connection.Open();
                LoadData();
            }
        }

        private void dgvAccountMa_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dgvAccountMa.CurrentRow.Index;
            txtAccount.Text = dgvAccountMa.Rows[i].Cells[0].Value.ToString();
            txtPws.Text = dgvAccountMa.Rows[i].Cells[1].Value.ToString();
            txtName.Text = dgvAccountMa.Rows[i].Cells[2].Value.ToString();
            txtEmail.Text = dgvAccountMa.Rows[i].Cells[3].Value.ToString();
            mtxtTell.Text = dgvAccountMa.Rows[i].Cells[4].Value.ToString();
            //dtpDate.Text = dgvAccountMa.Rows[i].Cells[5].Value.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            command = connection.CreateCommand();
            connection.Open();
                if (txtName.Text != "")
                {
                    
                    command.CommandText = "INSERT INTO Account(AccountName, Password, FullName, Email, Tell, DateCreated)" + "VALUES (N'" + txtAccount.Text + "', N'"
                    + txtPws.Text + "', N'" + txtName.Text + "', N'" + txtEmail.Text + "', '" + mtxtTell.Text + "', '" + dtpDate.Value.ToString() + "')";
                    command.ExecuteNonQuery();
                }
                LoadData();
            connection.Close();
        }

        private void xoaTaiKhoanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            connection = new SqlConnection(str);
            command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "DELETE from Account where AccountName= '" + txtAccount.Text + "'";
            command.ExecuteNonQuery();
            LoadData();
            connection.Close();
        }

        private void xemDanhSachVaiTroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRole role = new frmRole();
            role.Show(this);
        }
    }
}
