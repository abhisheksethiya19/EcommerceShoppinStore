using ECommerceShoppingStore.EFCore;
using ECommerceShoppingStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShoppingStore.DAL
{
    public class OrderDetailDAL
    {
        ECommerceShoppingStoreContext objECommerceShoppingStoreContext = new ECommerceShoppingStoreContext();

        public void CreateOrderDetails(OrderDetail objOrderDetails)
        {
            objECommerceShoppingStoreContext.Add(objOrderDetails);
            objECommerceShoppingStoreContext.SaveChanges();
        }

        public void UpdateOrderDetails(OrderDetail objbuyer)
        {
            objECommerceShoppingStoreContext.Entry(objbuyer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            objECommerceShoppingStoreContext.SaveChanges();
        }

        public void DeleteOrderDetails(int id)
        {
            OrderDetail objOrderDetails = objECommerceShoppingStoreContext.OrderDetails.Find(id);
            objECommerceShoppingStoreContext.Remove(objOrderDetails);
            objECommerceShoppingStoreContext.SaveChanges();
        }

        public OrderDetail GetOrderDetails(int id)
        {
            OrderDetail objOrderDetails = objECommerceShoppingStoreContext.OrderDetails.Find(id);
            return objOrderDetails;
        }

        public IEnumerable<OrderDetail> GetOrderDetails()
        {
            return objECommerceShoppingStoreContext.OrderDetails;
        }

    }
}

