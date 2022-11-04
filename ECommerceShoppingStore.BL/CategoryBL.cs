using ECommerceShoppingStore.Entities;
using ECommerceShoppingStore.DAL;


namespace ECommerceShoppingStore.BL
{
    public class CategoryBL
    {
        CategoryDAL objCategoryDAL = new CategoryDAL();

        public void CreateCategories(Category objCategory)
        {
            objCategoryDAL.CreateCategories(objCategory);
        }

        public void UpdateCategories(Category objCategory)
        {
            objCategoryDAL.UpdateCategories(objCategory);
        }

        public void DeleteCategories(int id)
        {
            objCategoryDAL.DeleteCategories(id);
        }

        public Category GetCategories(int id)
        {
            Category objCategory = objCategoryDAL.GetCategories(id);
            return objCategory;
        }

        public IEnumerable<Category> GetCategories()
        {
            return objCategoryDAL.GetCategories();
        }
    }
}

