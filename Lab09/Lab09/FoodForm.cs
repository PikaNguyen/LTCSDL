using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lab09.Models;

namespace Lab09
{
    public partial class FoodForm : Form
    {
        private RestaurantContext _dbContext;
        private int _foodId;
        public FoodForm(int? foodId = null)
        {
            InitializeComponent();
            _dbContext = new RestaurantContext();
            _foodId = foodId ?? 0;
        }

        private void FoodForm_Load(object sender, EventArgs e)
        {
            LoadCategoriesToComboBox();
            ShowFoodInformation();
        }

        private void ShowFoodInformation()
        {
            var food = GetFoodById(_foodId);
            if (food is null)
            {
                return;
            }

            txtID.Text = food.Id.ToString();
            txtName.Text = food.Name;
            cbbCatName.SelectedValue = food.FoodCategoryId;
            txtUnit.Text = food.Unit;
            nudPrice.Value = food.Price;
            txtNote.Text = food.Notes;
        }

        private void LoadCategoriesToComboBox()
        {
            var categories = _dbContext.Categories.OrderBy(c => c.Name).ToList();
            cbbCatName.DisplayMember = "Name";
            cbbCatName.ValueMember = "Id";
            cbbCatName.DataSource = categories;
        }
        private Food GetFoodById(int foodId)
        {
            return foodId > 0 ? _dbContext.Foods.Find(foodId) : null;
        }
        private bool ValidateUserInput()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Tên món ăn không được để trống", "Thông báo");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtUnit.Text))
            {
                MessageBox.Show("Đơn vị tính không được để trống", "Thông báo");
                return false;
            }

            if (nudPrice.Value == 0)
            {
                MessageBox.Show("Giá của thức ăn phải lớn hơn 0", "Thông báo");
                return false;
            }

            if (cbbCatName.SelectedIndex < 0)
            {
                MessageBox.Show("Bạn chưa chọn nhóm thức ăn", "Thông báo");
                return false;
            }

            return true;
        }
        private Food GetUpdatedFood()
        {
            var food = new Food()
            {
                Name = txtName.Text.Trim(),
                FoodCategoryId = (int)cbbCatName.SelectedValue,
                Unit = txtUnit.Text,
                Price = (int)nudPrice.Value,
                Notes = txtNote.Text
            };

            if (_foodId > 0)
            {
                food.Id = _foodId;
            }

            return food;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateUserInput())
            {
                var newFood = GetUpdatedFood();
                var oldFood = GetFoodById(_foodId);

                if (oldFood is null)
                {
                    _dbContext.Foods.Add(newFood);
                }
                else
                {
                    oldFood.Name = newFood.Name;
                    oldFood.FoodCategoryId = newFood.FoodCategoryId;
                    oldFood.Unit = newFood.Unit;
                    oldFood.Price = newFood.Price;
                    oldFood.Notes = newFood.Notes;
                }

                _dbContext.SaveChanges();

                DialogResult = DialogResult.OK;
            }
        }
    }
}
