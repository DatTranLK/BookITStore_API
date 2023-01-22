using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class ComboBook
    {
        public ComboBook()
        {
            DetailComboBooks = new HashSet<DetailComboBook>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? PriceReduction { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<DetailComboBook> DetailComboBooks { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
