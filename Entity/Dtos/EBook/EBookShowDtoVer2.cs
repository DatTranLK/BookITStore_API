using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.EBook
{
    public class EBookShowDtoVer2
    {
        public int EbookId { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string ImgPath { get; set; }
        public string CategoryName { get; set; }
        public string PublisherName { get; set; }
    }
}
