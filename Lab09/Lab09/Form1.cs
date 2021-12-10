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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ShowCategories();

        }
        private  List<Category> GetCategories()
        {
            var dbContext = new RestaurantContext();
            return dbContext.Categories.OrderBy(x => x.Name).ToList();
        }
        private void ShowCategories()
        {
            tvwCategory.Nodes.Clear();
            var cateMap = new Dictionary<CategoryType, string>()
            {
                [CategoryType.Food] = "Đồ ăn",
                [CategoryType.Drink] = "Thức uống"
            };
            var rootNode = tvwCategory.Nodes.Add("Tất cả");
            var categories = GetCategories();
            foreach (var cateType in cateMap)
            {
                var childNode = rootNode.Nodes.Add(cateType.Key.ToString(), cateType.Value);
                childNode.Tag = cateType.Key;
                foreach (var category in categories)
                {
                    if (category.Type != cateType.Key) continue;
                    var grantChildNode = childNode.Nodes.Add(category.Id.ToString(), category.Name);
                    grantChildNode.Tag = category;
                }
            }
            tvwCategory.ExpandAll();
            tvwCategory.SelectedNode = rootNode;
        }

        private void btnReloadCategory_Click(object sender, EventArgs e)
        {
            ShowCategories();
        }
        private List<FoodModels> GetFoodsByCategory(int? categoryId)
        {
            var dbContext = new RestaurantContext();
            var foods = dbContext.Foods.AsQueryable();

            if (categoryId != null && categoryId > 0)
            {
                foods = foods.Where(f => f.FoodCategoryId == categoryId);
            }

            return foods.OrderBy(f => f.Name)
                .Select(f => new FoodModels()
                {
                    Id = f.Id,
                    Name = f.Name,
                    Unit = f.Unit,
                    Price = f.Price,
                    Notes = f.Notes,
                    CategoryName = f.Category.Name
                }).ToList();
        }

        private List<FoodModels> GetFoodsByCategoryType(CategoryType categoryType)
        {
            var dbContext = new RestaurantContext();

            return dbContext.Foods
                .Where(f => f.Category.Type == categoryType)
                .Select(f => new FoodModels()
                {
                    Id = f.Id,
                    Name = f.Name,
                    Unit = f.Unit,
                    Price = f.Price,
                    Notes = f.Notes,
                    CategoryName = f.Category.Name
                }).ToList();
        }

        private void ShowFoodsForNode(TreeNode node)
        {
            lvFood.Items.Clear();

            if (node is null)
            {
                return;
            }

            List<FoodModels> foods = null;
            if (node.Level == 1)
            {
                var categoryType = (CategoryType)node.Tag;
                foods = GetFoodsByCategoryType(categoryType);
            }
            else
            {
                var category = node.Tag as Category;
                foods = GetFoodsByCategory(category?.Id);
            }

            ShowFoodsOnListView(foods);
        }

        private void ShowFoodsOnListView(List<FoodModels> foods)
        {
            foreach (var food in foods)
            {
                var item = lvFood.Items.Add(food.Id.ToString());
                item.SubItems.Add(food.Name);
                item.SubItems.Add(food.Unit);
                item.SubItems.Add(food.Price.ToString("##,###"));
                item.SubItems.Add(food.CategoryName);
                item.SubItems.Add(food.Notes);
            }
        }

        private void tvwCategory_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ShowFoodsForNode(e.Node);
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            var dialog = new UpdateCategory();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                ShowCategories();
            }
        }

        private void tvwCategory_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            if (e.Node is null || e.Node.Level < 2 || e.Node.Tag == null)
            {
                return;
            }

            var category = e.Node.Tag as Category;
            var dialog = new UpdateCategory(category?.Id);
            if (dialog.ShowDialog(this) == DialogResult.OK)
                ShowCategories();
        }

        private void btnReloadFood_Click(object sender, EventArgs e)
        {
            ShowFoodsForNode(tvwCategory.SelectedNode);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvFood.SelectedItems.Count == 0) return;
            var dbContext = new RestaurantContext();
            var selectedFoodID = int.Parse(lvFood.SelectedItems[0].Text);
            var selectedFood = dbContext.Foods.Find(selectedFoodID);
            if (selectedFood != null)
            {
                dbContext.Foods.Remove(selectedFood);
                dbContext.SaveChanges();
                lvFood.Items.Remove(lvFood.SelectedItems[0]);
            }
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            var dialog = new FoodForm();
            if ( dialog.ShowDialog(this)== DialogResult.OK) { ShowFoodsForNode(tvwCategory.SelectedNode); }
        }

        private void lvFood_DoubleClick(object sender, EventArgs e)
        {
            if (lvFood.SelectedItems.Count == 0) return;
            var foodID = int.Parse(lvFood.SelectedItems[0].Text);
            var dialog = new FoodForm(foodID);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                ShowFoodsForNode(tvwCategory.SelectedNode);
            }
        }
    }
}
