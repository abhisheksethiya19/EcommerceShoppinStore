using System;
using System.Collections.Generic;

namespace ECommerceShoppingStore.Entities
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
            ShoppingCarts = new HashSet<ShoppingCart>();
        }

        public int ProductsId { get; set; }
        public int CategoryId { get; set; }
        public string ModelNumber { get; set; } = null!;
        public string ModelName { get; set; } = null!;
        public decimal UnitCost { get; set; }
        public string Description { get; set; } = null!;

        public virtual Category? Category { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
