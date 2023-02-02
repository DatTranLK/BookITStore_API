using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class Ebook
    {
        public Ebook()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int BookId { get; set; }
        public int EbookId { get; set; }
        public int? Price { get; set; }
        public string PdfUrl { get; set; }
        public bool? HasPhysicalBook { get; set; }

        public virtual Book Book { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
