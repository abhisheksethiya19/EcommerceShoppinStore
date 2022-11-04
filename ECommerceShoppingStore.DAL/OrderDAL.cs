using ECommerceShoppingStore.EFCore;
using ECommerceShoppingStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShoppingStore.DAL
{
        public class OrderDAL
    {
        ECommerceShoppingStoreContext objECommerceShoppingStoreContext = new ECommerceShoppingStoreContext();

        public void CreateOrders(Order objOrders)
        {
            objECommerceShoppingStoreContext.Add(objOrders);
            objECommerceShoppingStoreContext.SaveChanges();
        }

        public void UpdateOrders(Order objbuyer)
        {
            objECommerceShoppingStoreContext.Entry(objbuyer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            objECommerceShoppingStoreContext.SaveChanges();
        }

        public void DeleteOrders(int id)
        {
            Order objOrders = objECommerceShoppingStoreContext.Orders.Find(id);
            objECommerceShoppingStoreContext.Remove(objOrders);
            objECommerceShoppingStoreContext.SaveChanges();
        }

        public Order GetOrders(int id)
        {
            Order objOrders = objECommerceShoppingStoreContext.Orders.Find(id);
            return objOrders;
        }

        public IEnumerable<Order> GetOrders()
        {
            return objECommerceShoppingStoreContext.Orders;
        }

    }
}

