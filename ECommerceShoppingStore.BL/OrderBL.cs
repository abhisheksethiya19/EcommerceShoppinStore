using ECommerceShoppingStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceShoppingStore.DAL;

namespace ECommerceShoppingStore.BL
{
    public class OrderBL
    {
        OrderDAL objOrderDAL = new OrderDAL();

        public void CreateOrder(Order objOrder)
        {
            objOrderDAL.CreateOrders(objOrder);
        }

        public void UpdateOrder(Order objOrder)
        {
            objOrderDAL.UpdateOrders(objOrder);
        }

        public void DeleteOrder(int id)
        {
            objOrderDAL.DeleteOrders(id);
        }

        public Order GetOrder(int id)
        {
            Order objOrder = objOrderDAL.GetOrders(id);
            return objOrder;
        }

        public IEnumerable<Order> GetOrders()
        {
            return objOrderDAL.GetOrders();
        }
    }
}

