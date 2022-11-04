using ECommerceShoppingStore.EFCore;
using ECommerceShoppingStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ECommerceShoppingStore.DAL
{
    public class ProductDAL
    {
        ECommerceShoppingStoreContext objECommerceShoppingStoreContext = new ECommerceShoppingStoreContext();

        public void CreateProducts(Product objProducts)
        {
            objECommerceShoppingStoreContext.Add(objProducts);
            objECommerceShoppingStoreContext.SaveChanges();
        }

        public void UpdateProducts(Product objbuyer)
        {
            objECommerceShoppingStoreContext.Entry(objbuyer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            objECommerceShoppingStoreContext.SaveChanges();
        }

        public void DeleteProducts(int id)
        {
            Product objProducts = objECommerceShoppingStoreContext.Products.Find(id);
            objECommerceShoppingStoreContext.Remove(objProducts);
            objECommerceShoppingStoreContext.SaveChanges();
        }

        public Product GetProducts(int id)
        {
            Product objProducts = objECommerceShoppingStoreContext.Products.Find(id);
            return objProducts;
        }

        public IEnumerable<Product> GetProducts()
        {
            return objECommerceShoppingStoreContext.Products;
        }

    }
}

