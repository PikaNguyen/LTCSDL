using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Cryptography;
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
            txtAccount.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            txtPws.Text = "";
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
            dtpDate.Text = dgvAccountMa.Rows[i].Cells[5].Value.ToString();
            txtTF.Text = dgvAccountMa.Rows[i].Cells[6].Value.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(newPassword);
            byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);
            string hasPass = "";
            foreach(var item in hasData) { hasPass += item; }
            connection = new SqlConnection(str);
            command = connection.CreateCommand();
            connection.Open();
                if (txtName.Text != "")
                {
                    
                    command.CommandText = "INSERT INTO Account(AccountName, Password, FullName, Email, Tell, DateCreated)" 
                    + "VALUES (N'" + txtAccount.Text + "', N'"
                    + hasPass.Length  + "', N'" + txtName.Text + "', N'" + txtEmail.Text + "', '" + mtxtTell.Text + "', '" + dtpDate.Value.ToString() + "')";
                    command.ExecuteNonQuery();
                }
                LoadData();
            connection.Close();
        }

        private void xoaTaiKhoanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = 0;
            connection = new SqlConnection(str);
            SqlCommand command1 = connection.CreateCommand();
            connection.Open();
            command1.CommandText = "UPDATE RoleAccount SET Actived= "+i+ " where [AccountName]= N'" + txtAccount.Text + "'";        
            command1.ExecuteNonQuery();
            connection.Close();

        }

        private void xemDanhSachVaiTroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtAccount.Text != "")
            {
                frmRole role = new frmRole();
                role.Show(this);
                role.LoadRole(txtAccount.Text);
            }
        }
        const string newPassword = "12345";  
        private void btnReset_Click(object sender, EventArgs e)
        {
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(newPassword);
            byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);
            string hasPass = "";
            foreach (var item in hasData) { hasPass += item; }
            connection = new SqlConnection(str);
            command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "UPDATE Account SET Password= N'" + hasPass + "' where [AccountName]= N'"+ txtAccount.Text + "'";
           var num = command.ExecuteNonQuery();
            if (num >= 1)
            {
                MessageBox.Show("Đã reset password!!");
                LoadData();
            }
            connection.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "UPDATE Account SET Password = N'"
                    + txtPws.Text + "', FullName= N'" + txtName.Text + "', Email= N'" + txtEmail.Text + "', Tell= '" + mtxtTell.Text + "', DateCreated= N'" + dtpDate.Value.ToString()+ "' where AccountName = N'" + txtAccount.Text+ "'";
            command.ExecuteNonQuery();
            LoadData();
        }

        private void btnState_Click(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "SELECT  Account.AccountName, Password, FullName, Email, Tell, DateCreated, Actived FROM Account, RoleAccount " +
                    " WHERE Account.AccountName = RoleAccount.AccountName" +
                    " ORDER BY RoleAccount.Actived";
            using (SqlDataAdapter ap = new SqlDataAdapter(command))
            {
                DataTable data = new DataTable();
                ap.Fill(data);
                dgvAccountMa.DataSource = data;
            }
            connection.Close();
        }
    }
}
