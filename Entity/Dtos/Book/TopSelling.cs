using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.Book
{
    public class TopSelling
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? AmountSold { get; set; }
    }
}
