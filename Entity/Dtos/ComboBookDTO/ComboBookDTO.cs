using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.ComboBookDTO
{
    public class ComboBookDTO
    {


        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? PriceReduction { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }

    }
}
