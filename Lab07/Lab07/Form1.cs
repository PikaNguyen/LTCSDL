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
    public partial class Form1 : Form
    {
        private DataTable foodTable;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.LoadCategory();
        }
        private void LoadCategory()
        {
            string connectionString = "server = .; database = RestaurantManagement; Integrated Security = true;";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText="Select ID, Name from Category";
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            connection.Open();
            adapter.Fill(dataTable);
            connection.Close();
            connection.Dispose();
            cbbCategory.DataSource = dataTable;
            cbbCategory.DisplayMember = "Name";
            cbbCategory.ValueMember = "ID";           
        }

        private void cbbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbCategory.SelectedIndex == -1 ) return;
            string connectionString = "server = .; database = RestaurantManagement; Integrated Security = true";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "Select * from Food where FoodCategoryID = @categoryId";
            command.Parameters.Add("@categoryId", SqlDbType.Int);
            if (cbbCategory.SelectedValue is DataRowView)
            {
                DataRowView rowView = cbbCategory.SelectedValue as DataRowView;
                command.Parameters["@categoryId"].Value = rowView["ID"];
            }
            else
            {
                command.Parameters["@categoryId"].Value = cbbCategory.SelectedValue;
            }
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            foodTable = new DataTable();
            connection.Open();
            adapter.Fill(foodTable);
            connection.Close();
            connection.Dispose();
            dgvFoodList.DataSource = foodTable;
            lblQuantity.Text = foodTable.Rows.Count.ToString();
            lblCatName.Text = cbbCategory.Text;

        }

        private void tsmCaculateQuantity_Click(object sender, EventArgs e)
        {
            string connectionString = "server = .; database = RestaurantManagement; Integrated Security = true";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "Select @numSaleFood  = sum(Quantity) from BillDetails where FoodID =@foodId";
            if (dgvFoodList.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvFoodList.SelectedRows[0];
                DataRowView rowView = selectedRow.DataBoundItem as DataRowView;
                command.Parameters.Add("@foodId", SqlDbType.Int);
                command.Parameters["@foodId"].Value = rowView["ID"];
                command.Parameters.Add("@numSaleFood", SqlDbType.Int);
                command.Parameters["@numSaleFood"].Direction = ParameterDirection.Output;
                connection.Open();
                command.ExecuteNonQuery();
                string result = command.Parameters["@numSaleFood"].Value.ToString();
                MessageBox.Show("Tổng số lượng món " + rowView["Name"] + " đã bán là: " + result + " " + rowView["unit"]);
                connection.Close();
            }
            connection.Dispose();
            command.Dispose();
        }

        private void tsmAddFood_Click(object sender, EventArgs e)
        {
            FoodInfoForm foodForm = new FoodInfoForm();
            foodForm.FormClosed += new FormClosedEventHandler(FoodForm_FormClosed);
            foodForm.Show(this);
            
        }

        private void FoodForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            int index = cbbCategory.SelectedIndex;
            cbbCategory.SelectedIndex = -1;
            cbbCategory.SelectedIndex = index;
            
        }

        private void tsmUpdateFood_Click(object sender, EventArgs e)
        {
            if (dgvFoodList.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvFoodList.SelectedRows[0];
                DataRowView rowView = selectedRow.DataBoundItem as DataRowView;
                FoodInfoForm foodForm = new FoodInfoForm();
                foodForm.FormClosed += new FormClosedEventHandler(FoodForm_FormClosed);
                foodForm.Show(this);
                foodForm.DisplayFoodInfo(rowView);
            }
        }

        private void txtSearchByName_TextChanged(object sender, EventArgs e)
        {
            if (foodTable == null) return;
            string filterExpression = "Name like '%" + txtSearchByName.Text + "%'";
            string sortExpression = "Price DESC";
            DataViewRowState rowStateFilter = DataViewRowState.OriginalRows;
            DataView foodView = new DataView(foodTable, filterExpression, sortExpression, rowStateFilter);
            dgvFoodList.DataSource = foodView;
        }

        private void tsmShowAccount_Click(object sender, EventArgs e)
        {
            AccountForm accountForm = new AccountForm();
            accountForm.Show(this);
        }

        private void tsmBill_Click(object sender, EventArgs e)
        {
            OrderForm order = new OrderForm();
            order.Show(this);
        }
    }
}
