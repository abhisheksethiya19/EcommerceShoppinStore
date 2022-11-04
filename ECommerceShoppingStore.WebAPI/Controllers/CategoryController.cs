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
    public class CategoryController : ControllerBase
    {
        private readonly CategoryBL categoryBL = new CategoryBL();

        public CategoryController()
        {

        }

        // Get All Category Details
        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]

        public ActionResult<IEnumerable<Category>> GetCategories()
        {
            return new ActionResult<IEnumerable<Category>>(categoryBL.GetCategories());

        }

        // Get Particular Category by using id
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<Category> GetCategories(int id)
        {
            var category = categoryBL.GetCategories(id);


            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // Edit Particular Category by using id
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult PutCategory(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            try
            {

                categoryBL.UpdateCategories(category);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategorytExists(id))
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


        // Create New Category type
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<Category> PostCategory(Category category)
        {


            try
            {

                categoryBL.CreateCategories(category);

            }
            catch (DbUpdateException)
            {
                if (CategorytExists(category.CategoryId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCategories", new { id = category.CategoryId }, category);
        }

        // Delete particular Category Details by using id
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<Category> Deletecategory(int id)
        {

            var category = categoryBL.GetCategories(id);

            if (category == null)
            {
                return NotFound();
            }

            try
            {
                categoryBL.DeleteCategories(category.CategoryId);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (CategorytExists(category.CategoryId))
                {
                    return NoContent();
                }
                else
                {
                    throw;
                }
            }

            return category;
        }

        private bool CategorytExists(int id)
        {


            if (categoryBL.GetCategories(id) != null)
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
