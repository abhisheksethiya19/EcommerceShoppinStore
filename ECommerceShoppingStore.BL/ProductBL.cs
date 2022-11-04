using ECommerceShoppingStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceShoppingStore.DAL;


namespace ECommerceShoppingStore.BL
{
    public class ProductBL
    {
        ProductDAL objProductDAL = new ProductDAL();

        public void CreateProduct(Product objProduct)
        {
            objProductDAL.CreateProducts(objProduct);
        }

        public void UpdateProduct(Product objProduct)
        {
            objProductDAL.UpdateProducts(objProduct);
        }

        public void DeleteProduct(int id)
        {
            objProductDAL.DeleteProducts(id);
        }

        public Product GetProduct(int id)
        {
            Product objProduct = objProductDAL.GetProducts(id);
            return objProduct;
        }

        public IEnumerable<Product> GetProducts()
        {
            return objProductDAL.GetProducts();
        }
    }
}

