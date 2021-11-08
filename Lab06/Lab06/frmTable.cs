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
    public partial class frmTable : Form
    {
        public frmTable()
        {
            InitializeComponent();
        }
        string str = "server = .; database = RestaurantManagement; Integrated Security = true;";
        SqlConnection connection;
        SqlCommand command2;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        void LoadData()
        {
            command2 = connection.CreateCommand();
            command2.CommandText = "SELECT * from [Table]";
            adapter.SelectCommand = command2;
            table.Clear();
            adapter.Fill(table);
            dgvTable.DataSource = table;
            connection.Close();
        }
        private void frmTable_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            LoadData();
        }

        private void dgvTable_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dgvTable.CurrentRow.Index;
            txtID.Text = dgvTable.Rows[i].Cells[0].Value.ToString();
            txtName.Text = dgvTable.Rows[i].Cells[1].Value.ToString();
            cbbStatus.Text = dgvTable.Rows[i].Cells[2].Value.ToString();
            txtCapa.Text = dgvTable.Rows[i].Cells[3].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            command2 = connection.CreateCommand();
            connection.Open();
            command2.CommandText = "Insert into [Table](Name, Status, Capacity) " +
                "values (N'"+txtName.Text+"', "+(cbbStatus.Text =="Trống" ? 0 : 1 )+", "+txtCapa.Text + " )";
            command2.ExecuteNonQuery();
            LoadData();
            connection.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            command2 = connection.CreateCommand();
            connection.Open();
            command2.CommandText = "DELETE from [Table] where ID= '" + txtID.Text + "'";
            command2.ExecuteNonQuery();
            LoadData();
            connection.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            command2 = connection.CreateCommand();
            connection.Open();
            command2.CommandText = "UPDATE [Table] SET Name = N'" + txtName.Text + "', Status = "
                   + (cbbStatus.Text == "Trống" ? 0 : 1) + ", Capacity= " + txtCapa.Text + " where [ID]= " + txtID.Text;
            command2.ExecuteNonQuery();
            using (SqlDataAdapter ap = new SqlDataAdapter(command2))
            {
                DataTable data = new DataTable();
                ap.Fill(data);
                dgvTable.DataSource = data;
            }
            LoadData();
            connection.Close();
        }
    }
}
