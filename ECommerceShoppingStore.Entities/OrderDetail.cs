using System;
using System.Collections.Generic;

namespace ECommerceShoppingStore.Entities
{
    public partial class OrderDetail
    {
        public OrderDetail()
        {
            ShoppingCarts = new HashSet<ShoppingCart>();
        }

        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitCost { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
