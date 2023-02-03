using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.Order
{
    public class OrderDtoForCus
    {
        public int Id { get; set; }
        public int? TotalPrice { get; set; }
        public DateTime? CreateDate { get; set; }
        public string ShippingAddress { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentMethod { get; set; }
    }
}
