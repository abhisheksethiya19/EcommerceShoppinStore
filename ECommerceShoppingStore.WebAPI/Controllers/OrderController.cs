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
    public class OrderController : ControllerBase
    {
        private readonly OrderBL orderBL = new OrderBL();

        public OrderController()
        {

        }

        // Get All Order Details
        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]

        public ActionResult<IEnumerable<Order>> GetOrder()
        {
            return new ActionResult<IEnumerable<Order>>(orderBL.GetOrders());

        }

        // Get Particular Order by using id
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult<Order> GetOrders(int id)
        {
            var order = orderBL.GetOrder(id);


            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // Edit Particular Order by using id
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Customer")]
        public IActionResult PutOrder(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            try
            {

                orderBL.UpdateOrder(order);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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


        // Create New Order type
        [HttpPost]
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult<Order> PostOrder(Order order)
        {


            try
            {

                orderBL.CreateOrder(order);

            }
            catch (DbUpdateException)
            {
                if (OrderExists(order.OrderId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        // Delete particular Category Details by using id
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult<Order> Deleteorders(int id)
        {

            var order = orderBL.GetOrder(id);

            if (order == null)
            {
                return NotFound();
            }

            try
            {
                orderBL.DeleteOrder(order.OrderId);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (OrderExists(order.OrderId))
                {
                    return NoContent();
                }
                else
                {
                    throw;
                }
            }

            return order;
        }

        private bool OrderExists(int id)
        {


            if (orderBL.GetOrder(id) != null)
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
