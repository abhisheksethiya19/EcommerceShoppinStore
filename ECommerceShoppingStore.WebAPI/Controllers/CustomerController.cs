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
    public class CustomerController : ControllerBase
    {
        private readonly CustomerBL customerBL = new CustomerBL();

        public CustomerController()
        {

        }

        // Get All Customer Details
        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]

        public ActionResult<IEnumerable<Customer>> GetCustomer()
        {
            return new ActionResult<IEnumerable<Customer>>(customerBL.GetCustomers());

        }

        // Get Perticular Customer by using id
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult<Customer> GetCustomer(int id)
        {
            var customer = customerBL.GetCustomer(id);


            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // Edit Perticular Customer by using id
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Customer")]
        public IActionResult PutCustomer(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            try
            {

                customerBL.UpdateCustomer(customer);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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


        // Create New Customer type
        [HttpPost]
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult<Customer> PostCustomer(Customer customer)
        {


            try
            {

                customerBL.CreateCustomer(customer);

            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.CustomerId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        // delete perticular Customer Details by using id
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult<Customer> Deletecustomer(int id)
        {

            var customer = customerBL.GetCustomer(id);

            if (customer == null)
            {
                return NotFound();
            }

            try
            {
                customerBL.DeleteCustomer(customer.CustomerId);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (CustomerExists(customer.CustomerId))
                {
                    return NoContent();
                }
                else
                {
                    throw;
                }
            }

            return customer;
        }

        private bool CustomerExists(int id)
        {


            if (customerBL.GetCustomer(id) != null)
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
