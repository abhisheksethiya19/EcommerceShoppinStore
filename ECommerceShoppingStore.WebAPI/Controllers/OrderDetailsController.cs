using ECommerceShoppingStore.BL;
using ECommerceShoppingStore.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceShoppingStore.WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly OrderDetailBL orderDetailBL = new OrderDetailBL();

        public OrderDetailsController()
        {

        }

        // Get All Order Details
        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]

        public ActionResult<IEnumerable<OrderDetail>> GetOrderDetail()
        {
            return new ActionResult<IEnumerable<OrderDetail>>(orderDetailBL.GetOrderDetails());

        }

        // Get Particular Order by using id
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult<OrderDetail> GetOrderDetails(int id)
        {
            var orderDetail = orderDetailBL.GetOrderDetail(id);


            if (orderDetail == null)
            {
                return NotFound();
            }

            return orderDetail;
        }

        // Edit Particular OrderDetail by using id
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Customer")]
        public IActionResult PutOrderDetail(int id, OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderId)
            {
                return BadRequest();
            }

            try
            {

                orderDetailBL.UpdateOrderDetail(orderDetail);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // Create New OrderDetail type
        [HttpPost]
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult<OrderDetail> PostOrderDetail(OrderDetail orderDetail)
        {


            try
            {

                orderDetailBL.CreateOrderDetail(orderDetail);

            }
            catch (DbUpdateException)
            {
                if (OrderDetailExists(orderDetail.OrderId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrderDetails", new { id = orderDetail.OrderId }, orderDetail);
        }

        // Delete particular  OrderDetails by using id
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult<OrderDetail> DeleteorderDetails(int id)
        {

            var orderDetail = orderDetailBL.GetOrderDetail(id);

            if (orderDetail == null)
            {
                return NotFound();
            }

            try
            {
                orderDetailBL.DeleteOrderDetail(orderDetail.OrderId);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (OrderDetailExists(orderDetail.OrderId))
                {
                    return NoContent();
                }
                else
                {
                    throw;
                }
            }

            return orderDetail;
        }

        private bool OrderDetailExists(int id)
        {


            if (orderDetailBL.GetOrderDetail(id) != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
