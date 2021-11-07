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
    public partial class frmFood : Form
    {
        public frmFood()
        {
            InitializeComponent();
        }
        public void LoadFood(int categoryID)
        {
            string connectionString = "server =. ; database = RestaurantManagement; Integrated Security = true;";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = "Select Name from Category where ID = " + categoryID;
            sqlConnection.Open();
            string catName = sqlCommand.ExecuteScalar().ToString();
            this.Text = "Danh sách các món ăn thuộc nhóm: " + catName;
            sqlCommand.CommandText = "Select * from Food where FoodCategoryID = " + categoryID;
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable("Food");
            da.Fill(dt);
            dgvFood.DataSource = dt;
            sqlConnection.Close();
            sqlConnection.Dispose();
            da.Dispose();
        }
        string str = "server = .; database = RestaurantManagement; Integrated Security = true;";
        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        void LoadData()
        {
            command = connection.CreateCommand();
            command.CommandText = "SELECT * from Food";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgvFood.DataSource = table;
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection =new SqlConnection(str)) {
                command = connection.CreateCommand();

                if (txtID.Text == "")
                {
                    connection.Open();
                    command.CommandText = "INSERT INTO Food(Name, Unit, FoodCategoryID, Price, Notes)" + "VALUES (N'" + txtName.Text + "', N'"
                    + txtUnit.Text + "', '" + txtFoodCategoryID.Text + "', '" + txtPrice.Text + "', '" + txtNote.Text + "')";
                    command.ExecuteNonQuery();
                }
                else
                {
                    connection.Open();
                    command.CommandText = "UPDATE Food SET Name= N'" + txtName.Text + "', Unit= N'"
                   + txtUnit.Text + "', FoodCategoryID= " + txtFoodCategoryID.Text + ", Price= "
                   + txtPrice.Text + ", Notes= N'" + txtNote.Text + "' where [ID]= " + txtID.Text;
                    using (SqlDataAdapter ap = new SqlDataAdapter(command))
                    {
                        DataTable data = new DataTable();
                        ap.Fill(data);
                        dgvFood.DataSource = data;
                    }            
                }
                LoadData();
            }        
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {         
            command = connection.CreateCommand();
            command.CommandText = "DELETE from Food where ID= '" + txtID.Text + "'";
            command.ExecuteNonQuery();
            LoadData();
            connection.Close();
        }

        private void dgvFood_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dgvFood.CurrentRow.Index;
            txtID.Text = dgvFood.Rows[i].Cells[0].Value.ToString();
            txtName.Text = dgvFood.Rows[i].Cells[1].Value.ToString();
            txtUnit.Text = dgvFood.Rows[i].Cells[2].Value.ToString();
            txtFoodCategoryID.Text = dgvFood.Rows[i].Cells[3].Value.ToString();
            txtPrice.Text = dgvFood.Rows[i].Cells[4].Value.ToString();
            txtNote.Text = dgvFood.Rows[i].Cells[5].Value.ToString();
        }
        private void frmFood_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            LoadData();
        }
    }
}
