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
    public class ShoppingCartController : ControllerBase
    {
        private readonly ShoppingCartBL shoppingCartBL = new ShoppingCartBL();

        public ShoppingCartController()
        {

        }

        // Get All ShoppingCart Details
        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]

        public ActionResult<IEnumerable<ShoppingCart>> GetShoppingCart()
        {
            return new ActionResult<IEnumerable<ShoppingCart>>(shoppingCartBL.GetShoppingCarts());

        }

        // Get Perticular ShoppingCart by using id
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult<ShoppingCart> GetShoppingCart(int id)
        {
            var shoppingCart = shoppingCartBL.GetShoppingCart(id);


            if (shoppingCart == null)
            {
                return NotFound();
            }

            return shoppingCart;
        }

        // Edit Perticular ShoppingCart by using id
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Customer")]
        public IActionResult PutShoppingCart(int id, ShoppingCart shoppingCart)
        {
            if (id != shoppingCart.RecordId)
            {
                return BadRequest();
            }

            try
            {

                shoppingCartBL.UpdateShoppingCart(shoppingCart);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingCartExists(id))
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


        // Create New ShoppingCart type
        [HttpPost]
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult<ShoppingCart> PostShoppingCart(ShoppingCart shoppingCart)
        {


            try
            {

                shoppingCartBL.CreateShoppingCart(shoppingCart);

            }
            catch (DbUpdateException)
            {
                if (ShoppingCartExists(shoppingCart.RecordId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetShoppingCart", new { id = shoppingCart.RecordId }, shoppingCart);
        }

        // delete perticular ShoppingCart Details by using id
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult<ShoppingCart> DeleteshoppingCart(int id)
        {

            var shoppingCart = shoppingCartBL.GetShoppingCart(id);

            if (shoppingCart == null)
            {
                return NotFound();
            }

            try
            {
                shoppingCartBL.DeleteShoppingCarts(shoppingCart.RecordId);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (ShoppingCartExists(shoppingCart.RecordId))
                {
                    return NoContent();
                }
                else
                {
                    throw;
                }
            }

            return shoppingCart;
        }

        private bool ShoppingCartExists(int id)
        {


            if (shoppingCartBL.GetShoppingCart(id) != null)
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
