using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public decimal? TotalPrice { get; set; }
        public DateTime? CreateDate { get; set; }
        public string ShippingAddress { get; set; }
        public string OrderStatus { get; set; }
        public int? CustomerId { get; set; }

        public virtual Account Customer { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
