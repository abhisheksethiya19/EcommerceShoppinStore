using ECommerceShoppingStore.EFCore;
using ECommerceShoppingStore.Entities;
namespace ECommerceShoppingStore.DAL
{
    public class CategoryDAL
    {
        ECommerceShoppingStoreContext objECommerceShoppingStoreContext = new ECommerceShoppingStoreContext();

        public void CreateCategories(Category objCategories)
        {
            objECommerceShoppingStoreContext.Add(objCategories);
            objECommerceShoppingStoreContext.SaveChanges();
        }

        public void UpdateCategories(Category objbuyer)
        {
            objECommerceShoppingStoreContext.Entry(objbuyer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            objECommerceShoppingStoreContext.SaveChanges();
        }

        public void DeleteCategories(int id)
        {
            Category objCategories = objECommerceShoppingStoreContext.Categories.Find(id);
            objECommerceShoppingStoreContext.Remove(objCategories);
            objECommerceShoppingStoreContext.SaveChanges();
        }

        public Category GetCategories(int id)
        {
            Category objCategories = objECommerceShoppingStoreContext.Categories.Find(id);
            return objCategories;
        }

        public IEnumerable<Category> GetCategories()
        {
            return objECommerceShoppingStoreContext.Categories;
        }

    }
}

