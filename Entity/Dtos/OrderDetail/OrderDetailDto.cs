using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.OrderDetail
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int? EbookId { get; set; }
        public int? BookId { get; set; }
        public int? ComboBookId { get; set; }
        public int? Quantity { get; set; }

        public string? BookName { get; set; }
        public decimal? PriceBook { get; set; }
        public string? EBookName { get; set; }
        public int? PriceEBook { get; set; }
        public string? ComboBookName { get; set; }
        public decimal? PriceCombo { get; set; }
    }
}
