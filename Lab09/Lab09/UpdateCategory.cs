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
    public partial class UpdateCategory : Form
    {
        private RestaurantContext _dbContext;
        private int _categoryID;
        public UpdateCategory(int ? categoryId = null)
        {
            InitializeComponent();
            _dbContext = new RestaurantContext();
            _categoryID = categoryId ?? 0;
        }
        private Category GetCategoryById(int categoryId)
        {
            return categoryId > 0 ? _dbContext.Categories.Find(categoryId) : null;
        }

        private void ShowCategory()
        {
            var category = GetCategoryById(_categoryID);
            if (category == null)
            {
                return;
            }

            txtCategoryID.Text = category.Id.ToString();
            txtCategoryName.Text = category.Name;
            cbbCategoryType.SelectedIndex = (int)category.Type;
        }
        private Category GetUpdatedCategory()
        {
            var category = new Category()
            {
                Name = txtCategoryName.Text.Trim(),
                Type = (CategoryType)cbbCategoryType.SelectedIndex
            };

            if (_categoryID > 0)
            {
                category.Id = _categoryID;
            }

            return category;
        }

        private bool ValidateUserInput()
        {
            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                MessageBox.Show("Tên nhóm thức ăn không được để trống", "Thông báo");
                return false;
            }

            if (cbbCategoryType.SelectedIndex < 0)
            {
                MessageBox.Show("Bạn chưa chọn loại nhóm món ăn", "Thông báo");
                return false;
            }

            return true;
        }


        private void UpdateCategory_Load(object sender, EventArgs e)
        {
            ShowCategory();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateUserInput())
            {
                var newCategory = GetUpdatedCategory();
                var oldCategory = GetCategoryById(_categoryID);

                if (oldCategory is null)
                {
                    _dbContext.Categories.Add(newCategory);
                }
                else
                {
                    oldCategory.Name = newCategory.Name;
                    oldCategory.Type = newCategory.Type;
                }

                _dbContext.SaveChanges();
                DialogResult = DialogResult.OK;
            }
        }
    }
}
