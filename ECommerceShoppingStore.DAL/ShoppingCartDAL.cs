using ECommerceShoppingStore.EFCore;
using ECommerceShoppingStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShoppingStore.DAL
{
    public class ShoppingCartDAL
    {
        ECommerceShoppingStoreContext objECommerceShoppingStoreContext = new ECommerceShoppingStoreContext();

        public void CreateShoppingCart(ShoppingCart objshoppingCart)
        {
            objECommerceShoppingStoreContext.Add(objshoppingCart);
            objECommerceShoppingStoreContext.SaveChanges();
        }

        public void UpdateShoppingCart(ShoppingCart objshoppingCart)
        {
            objECommerceShoppingStoreContext.Entry(objshoppingCart).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            objECommerceShoppingStoreContext.SaveChanges();
        }

        public void DeleteShoppingCarts(int id)
        {
            ShoppingCart objshoppingCart = objECommerceShoppingStoreContext.ShoppingCarts.Find(id);
            objECommerceShoppingStoreContext.Remove(objshoppingCart);
            objECommerceShoppingStoreContext.SaveChanges();
        }

        public ShoppingCart GetShoppingCart(int id)
        {
            ShoppingCart objshoppingCart = objECommerceShoppingStoreContext.ShoppingCarts.Find(id);
            return objshoppingCart;
        }

        public IEnumerable<ShoppingCart> GetShoppingCarts()
        {
            return objECommerceShoppingStoreContext.ShoppingCarts;
        }

    }
}