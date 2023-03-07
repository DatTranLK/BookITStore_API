using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.EBook
{
    public class EBookDtoForAdmin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Isbn { get; set; }
        public string Author { get; set; }
        public int? PriceEbook { get; set; }
        public int? Amount { get; set; }
        public int? AmountSold { get; set; }
        public bool? IsActive { get; set; }
        public string CategoryName { get; set; }
        public string PublisherName { get; set; }
    }
}
