using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.Book
{
    public class BookShowDtoVer2
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string ImgPath { get; set; }
        public string CategoryName { get; set; }
        public string PublisherName { get; set; }
        public int? Amount { get; set; }
    }
}
