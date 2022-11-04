using ECommerceShoppingStore.DAL;
using ECommerceShoppingStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ECommerceShoppingStore.BL
{
    public class ShoppingCartBL
    {
        ShoppingCartDAL objShoppingCartDAL = new ShoppingCartDAL();

        public void CreateShoppingCart(ShoppingCart objShoppingCart)
        {
            objShoppingCartDAL.CreateShoppingCart(objShoppingCart);
        }

        public void UpdateShoppingCart(ShoppingCart objShoppingCart)
        {
            objShoppingCartDAL.UpdateShoppingCart(objShoppingCart);
        }

        public void DeleteShoppingCarts(int id)
        {
            objShoppingCartDAL.DeleteShoppingCarts(id);
        }

        public ShoppingCart GetShoppingCart(int id)
        {
            ShoppingCart objShoppingCart = objShoppingCartDAL.GetShoppingCart(id);
            return objShoppingCart;
        }

        public IEnumerable<ShoppingCart> GetShoppingCarts()
        {
            return objShoppingCartDAL.GetShoppingCarts();
        }
    }
}
