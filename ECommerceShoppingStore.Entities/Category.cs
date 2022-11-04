using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerceShoppingStore.Entities
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }
        [Required(ErrorMessage = "CategoryId is required")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        //[StringLength(2,MinimumLength = 4)]
        public string CategoryName { get; set; } = null!;

        [Required(ErrorMessage = "Description is required")]

        public string Description { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
