using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int? BookId { get; set; }
        public int? ComboBookId { get; set; }
        public int? Quantity { get; set; }

        public virtual Book Book { get; set; }
        public virtual ComboBook ComboBook { get; set; }
        public virtual Order Order { get; set; }
    }
}
