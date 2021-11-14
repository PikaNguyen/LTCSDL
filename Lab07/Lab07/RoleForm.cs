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
    public partial class RoleForm : Form
    {
        public RoleForm()
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
        public void LoadRole(string accountN)
        {
            connection = new SqlConnection(str);
            command = connection.CreateCommand();
            command.CommandText = "SELECT a.AccountName, r.RoleName, r.Path, r.Notes " +
                 " from Role r, RoleAccount ra, Account a" +
                 " Where a.AccountName = ra.AccountName and r.ID = ra.RoleID and a.AccountName = '" + accountN + "'";
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable("Role");
            adapter.Fill(table);
            dgvRole.DataSource = table;
            dgvRole.Columns[0].ReadOnly = true;
            connection.Close();
        }

        private void btnAddRole_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "server=.;database = RestaurantManagement; Integrated Security = true;";
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Execute InsertRole @ID OUTPUT, @name, @path, @notes";

                cmd.Parameters.Add("@ID", SqlDbType.Int);
                cmd.Parameters.Add("@name", SqlDbType.NVarChar, 1000);
                cmd.Parameters.Add("@path", SqlDbType.NVarChar, 100);
                cmd.Parameters.Add("@notes", SqlDbType.NVarChar, 3000);

                cmd.Parameters["@ID"].Direction = ParameterDirection.Output;

                cmd.Parameters["@name"].Value = txtName.Text;
                cmd.Parameters["@path"].Value = txtPath.Text;
                cmd.Parameters["@notes"].Value = txtNotes.Text;
                conn.Open();
                int numRowAffected = cmd.ExecuteNonQuery();
                if (numRowAffected > 0)
                {
                    string roleID = cmd.Parameters["@ID"].Value.ToString();
                    MessageBox.Show("Thêm vai trò thành công  = " + roleID, "Message!!!!");
                    this.ResetText();
                }
                else
                {
                    MessageBox.Show("Thêm thất bại");
                }
                conn.Close();
                conn.Dispose();
            }
            catch (SqlException exception)
            {
                MessageBox.Show(exception.Message, "SQL Error");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error");
            }
        }

        private void dgvRole_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dgvRole.CurrentRow.Index;
            //txtName.Text = dgvRole.Rows[i].Cells["AccountName"].ToString();
            txtPath.Text = dgvRole.Rows[i].Cells[1].Value.ToString();
            txtNotes.Text = dgvRole.Rows[i].Cells[2].Value.ToString();
        }

        private void RoleForm_Load(object sender, EventArgs e)
        {
            //connection = new SqlConnection(str);
           // connection.Open();
            //LoadData();
            //connection.Close();
        }
    }
}
