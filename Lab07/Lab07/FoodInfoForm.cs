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
    public partial class FoodInfoForm : Form
    {
        public FoodInfoForm()
        {
            InitializeComponent();
        }

        private void FoodInfoForm_Load(object sender, EventArgs e)
        {
            this.InitValues();
        }

        private void InitValues()
        {
            string connectionString = "server=.; database = RestaurantManagement; Integrated Security = true;";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "Select ID, Name from Category";
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            connection.Open();
            adapter.Fill(ds,"Category");
            cbbCatName.DataSource = ds.Tables["Category"];
            cbbCatName.DisplayMember = "Name";
            cbbCatName.ValueMember = "ID";
            connection.Close();
            connection.Dispose();
        }
        private void ResetText()
        {
            txtID.ResetText();
            txtName.ResetText();
            txtUnit.ResetText();
            txtNote.ResetText();
            cbbCatName.ResetText();
            nudPrice.ResetText();
        }
        private bool checkData()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Chưa nhập tên món ăn !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;

            }
            else if (string.IsNullOrWhiteSpace(txtUnit.Text))
            {
                MessageBox.Show("Chưa nhập đơn vị cho món ăn !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUnit.Focus();
                return false;
            }
            else if (nudPrice.Value < 1000)
            {
                MessageBox.Show("Chưa nhập hoặc giá không hợp lý cho món ăn !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudPrice.Focus();
                return false;
            }
            return true;
        }
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            checkData();
            try
            {
                string connectionString = "server=.;database = RestaurantManagement; Integrated Security = true;";
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Execute InsertFood @ID OUTPUT, @name, @unit, @foodCategoryID, @price, @notes";
               
                cmd.Parameters.Add("@ID", SqlDbType.Int);
                cmd.Parameters.Add("@name", SqlDbType.NVarChar, 1000);
                cmd.Parameters.Add("@unit", SqlDbType.NVarChar, 100);
                cmd.Parameters.Add("@foodCategoryID", SqlDbType.Int);
                cmd.Parameters.Add("@price", SqlDbType.Int);
                cmd.Parameters.Add("@notes", SqlDbType.NVarChar, 3000);

                cmd.Parameters["@ID"].Direction = ParameterDirection.Output;

                cmd.Parameters["@name"].Value = txtName.Text;
                cmd.Parameters["@unit"].Value = txtUnit.Text;
                cmd.Parameters["@foodCategoryID"].Value = cbbCatName.SelectedValue;
                cmd.Parameters["@price"].Value = nudPrice.Value;
                cmd.Parameters["@notes"].Value = txtNote.Text;
                conn.Open();
                int numRowAffected = cmd.ExecuteNonQuery();
                if (numRowAffected > 0)
                {
                    string foodID = cmd.Parameters["@ID"].Value.ToString();
                    MessageBox.Show("Successfully adding new food, Food ID = " + foodID, "Message!!!!");
                    this.ResetText();                   
                }
                else
                {
                    MessageBox.Show("Adding food failed");
                }
                conn.Close();
                conn.Dispose();
            }
            catch (SqlException exception)
            { 
                   MessageBox.Show(exception.Message,"SQL Error");
            }
            catch ( Exception exception)
            {
                MessageBox.Show(exception.Message, "Error");
            }
            
        }
        public void DisplayFoodInfo(DataRowView rowView)
        {
            try
            {
                txtID.Text = rowView["ID"].ToString();
                txtName.Text = rowView["Name"].ToString();
                txtUnit.Text = rowView["Unit"].ToString();
                txtNote.Text = rowView["Notes"].ToString();
                nudPrice.Text = rowView["Price"].ToString();
                cbbCatName.SelectedIndex = -1;
                for (int index = 0; index < cbbCatName.Items.Count; index++)
                {
                    DataRowView cat = cbbCatName.Items[index] as DataRowView;
                    if (cat["ID"].ToString() == rowView["FoodCategoryID"].ToString())
                    {
                        cbbCatName.SelectedIndex = index;
                        break;
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error!!");
                this.Close();
            }
        }

        private void btnUpdateFood_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "server=.;database = RestaurantManagement; Integrated Security = true;";
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Execute UpdateFood @ID, @name, @unit, @foodCategoryID, @price, @notes";
                
                cmd.Parameters.Add("@ID", SqlDbType.Int);
                cmd.Parameters.Add("@name", SqlDbType.NVarChar, 1000);
                cmd.Parameters.Add("@unit", SqlDbType.NVarChar, 100);
                cmd.Parameters.Add("@foodCategoryID", SqlDbType.Int);
                cmd.Parameters.Add("@price", SqlDbType.Int);
                cmd.Parameters.Add("@notes", SqlDbType.NVarChar, 3000);

                cmd.Parameters["@id"].Value = int.Parse(txtID.Text);
                cmd.Parameters["@name"].Value = txtName.Text;
                cmd.Parameters["@unit"].Value = txtUnit.Text;
                cmd.Parameters["@foodCategoryID"].Value = cbbCatName.SelectedValue;
                cmd.Parameters["@price"].Value = nudPrice.Value;
                cmd.Parameters["@notes"].Value = txtNote.Text;
                conn.Open();
                int numRowAffected = cmd.ExecuteNonQuery();
                if (numRowAffected > 0)
                {
                    string foodID = cmd.Parameters["@id"].Value.ToString();
                    MessageBox.Show("Successfully updating food", "Message!!!!");
                    this.ResetText();
                }
                else
                {
                    MessageBox.Show("Updating food failed");
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            AddCategoryFood add = new AddCategoryFood();
            if (add.ShowDialog() == DialogResult.OK)
            {
                InitValues();
            }
        }
    }
    
}
