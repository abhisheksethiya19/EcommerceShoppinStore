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
    public class ProductController : ControllerBase
    {
        private readonly ProductBL productBL = new ProductBL();

        public ProductController()
        {

        }

        // Get All Product Details
        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]

        public ActionResult<IEnumerable<Product>> GetProduct()
        {
            return new ActionResult<IEnumerable<Product>>(productBL.GetProducts());

        }

        // Get Perticular Product by using id
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = productBL.GetProduct(id);


            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // Edit Perticular Product by using id
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Customer")]
        public IActionResult PutProduct(int id, Product product)
        {
            if (id != product.ProductsId)
            {
                return BadRequest();
            }

            try
            {

                productBL.UpdateProduct(product);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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


        // Create New Product type
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<Product> PostProduct(Product product)
        {


            try
            {

                productBL.CreateProduct(product);

            }
            catch (DbUpdateException)
            {
                if (ProductExists(product.ProductsId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProduct", new { id = product.ProductsId }, product);
        }

        // delete perticular Customer Details by using id
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<Product> Deleteproduct(int id)
        {

            var product = productBL.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            try
            {
                productBL.DeleteProduct(product.ProductsId);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (ProductExists(product.ProductsId))
                {
                    return NoContent();
                }
                else
                {
                    throw;
                }
            }

            return product;
        }

        private bool ProductExists(int id)
        {


            if (productBL.GetProduct(id) != null)
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
