using ECommerceShoppingStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceShoppingStore.DAL;

namespace ECommerceShoppingStore.BL
{
      public class CustomerBL
    {
        CustomerDAL objCustomerDAL = new CustomerDAL();

        public void CreateCustomer(Customer objCustomer)
        {
            objCustomerDAL.CreateCustomers(objCustomer);
        }

        public void UpdateCustomer(Customer objCustomer)
        {
            objCustomerDAL.UpdateCustomers(objCustomer);
        }

        public void DeleteCustomer(int id)
        {
            objCustomerDAL.DeleteCustomers(id);
        }

        public Customer GetCustomer(int id)
        {
            Customer objCustomer = objCustomerDAL.GetCustomers(id);
            return objCustomer;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return objCustomerDAL.GetCustomers();
        }
    }
}

