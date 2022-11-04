using ECommerceShoppingStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceShoppingStore.DAL;

namespace ECommerceShoppingStore.BL
{
    public class OrderDetailBL
    {
        OrderDetailDAL objOrderDetailDAL = new OrderDetailDAL();

        public void CreateOrderDetail(OrderDetail objOrderDetail)
        {
            objOrderDetailDAL.CreateOrderDetails(objOrderDetail);
        }

        public void UpdateOrderDetail(OrderDetail objOrderDetail)
        {
            objOrderDetailDAL.UpdateOrderDetails(objOrderDetail);
        }

        public void DeleteOrderDetail(int id)
        {
            objOrderDetailDAL.DeleteOrderDetails(id);
        }

        public OrderDetail GetOrderDetail(int id)
        {
            OrderDetail objOrderDetail = objOrderDetailDAL.GetOrderDetails(id);
            return objOrderDetail;
        }

        public IEnumerable<OrderDetail> GetOrderDetails()
        {
            return objOrderDetailDAL.GetOrderDetails();
        }
    }
}

