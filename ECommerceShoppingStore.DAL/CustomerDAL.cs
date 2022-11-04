using ECommerceShoppingStore.EFCore;
using ECommerceShoppingStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShoppingStore.DAL
{
    public class CustomerDAL
    {
        ECommerceShoppingStoreContext objECommerceShoppingStoreContext = new ECommerceShoppingStoreContext();

        public void CreateCustomers(Customer objCustomer)
        {
            objECommerceShoppingStoreContext.Add(objCustomer);
            objECommerceShoppingStoreContext.SaveChanges();
        }

        public void UpdateCustomers(Customer objCustomer)
        {
            objECommerceShoppingStoreContext.Entry(objCustomer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            objECommerceShoppingStoreContext.SaveChanges();
        }

        public void DeleteCustomers(int id)
        {
            Customer objCustomer = objECommerceShoppingStoreContext.Customers.Find(id);
            objECommerceShoppingStoreContext.Remove(objCustomer);
            objECommerceShoppingStoreContext.SaveChanges();
        }

        public Customer GetCustomers(int id)
        {
            Customer objCustomer = objECommerceShoppingStoreContext.Customers.Find(id);
            return objCustomer;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return objECommerceShoppingStoreContext.Customers;
        }

    }
}

