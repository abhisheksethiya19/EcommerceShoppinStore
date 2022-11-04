using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerceShoppingStore.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        [Required(ErrorMessage = "CustomerId is required")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "EmailAddress  is required")]
        public string EmailAddress { get; set; } = null!;

        [Required(ErrorMessage = "Password  is required")]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "DeliveryAddress is required")]
        public string DeliveryAddress { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
