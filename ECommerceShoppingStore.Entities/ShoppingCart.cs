using System;
using System.Collections.Generic;

namespace ECommerceShoppingStore.Entities
{
    public partial class ShoppingCart
    {
        public int RecordId { get; set; }
        public int CartId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual OrderDetail QuantityNavigation { get; set; } = null!;
    }
}
