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

namespace Lab07
{
    public partial class AccountForm : Form
    {
        string str = "server = .; database = RestaurantManagement; Integrated Security = true;";
        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        const string newPassword = "12345";
        public AccountForm()
        {
            InitializeComponent();
        }
        void LoadData()
        {
            command = connection.CreateCommand();
            command.CommandText = "SELECT Account.AccountName, FullName, Email, Tell, DateCreated, Actived FROM Account, RoleAccount " +
                    " WHERE Account.AccountName = RoleAccount.AccountName" +
                    " ORDER BY RoleAccount.Actived";

            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgvAccountMa.DataSource = table;
        }

        private void AccountForm_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            this.LoadData();
            connection.Close();
            connection.Dispose();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "server=.;database = RestaurantManagement; Integrated Security = true;";
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "EXECUTE InsertAccount @AccountName,@Pass,@FullName,@Email,@Tell,@DateCreated";

                cmd.Parameters.Add("@AccountName", SqlDbType.NVarChar, 100);
                cmd.Parameters.Add("@Pass", SqlDbType.NVarChar, 100);
                cmd.Parameters.Add("@FullName", SqlDbType.NVarChar, 100);
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
                cmd.Parameters.Add("@Tell", SqlDbType.NVarChar, 20);
                cmd.Parameters.Add("@DateCreated", SqlDbType.SmallDateTime);

                cmd.Parameters["@AccountName"].Value = txtAccount.Text;
                cmd.Parameters["@Pass"].Value = newPassword;
                cmd.Parameters["@FullName"].Value = txtName.Text;
                cmd.Parameters["@Email"].Value = txtEmail.Text;
                cmd.Parameters["@Tell"].Value = mtxtTell.Text;
                cmd.Parameters["@DateCreated"].Value = DateTime.Now.ToShortDateString();

                conn.Open();

                int numOfRows = cmd.ExecuteNonQuery();

                if (numOfRows == 1)
                {
                    LoadData();
                    MessageBox.Show("Thêm tài khoản thành công");
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                }
                conn.Close();
                conn.Dispose();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error");
            }
        }

        private void dgvAccountMa_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dgvAccountMa.CurrentRow.Index;
            txtAccount.Text = dgvAccountMa.Rows[i].Cells[0].Value.ToString();
            txtName.Text = dgvAccountMa.Rows[i].Cells[1].Value.ToString();
            txtEmail.Text = dgvAccountMa.Rows[i].Cells[2].Value.ToString();
            mtxtTell.Text = dgvAccountMa.Rows[i].Cells[3].Value.ToString();
            dtpDate.Text = dgvAccountMa.Rows[i].Cells[4].Value.ToString();
            txtTF.Text = dgvAccountMa.Rows[i].Cells[5].Value.ToString();
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void tsmAccountRole_Click(object sender, EventArgs e)
        {
            if (txtAccount.Text != "")
            {
                RoleForm role = new RoleForm();
                role.Show(this);
                role.LoadRole(txtAccount.Text);
            }
        }
    }
}
