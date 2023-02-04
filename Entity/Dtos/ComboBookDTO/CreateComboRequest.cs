using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.ComboBookDTO
{
    public class CreateComboRequest
    {
        public ComboBook comboBook { get; set; }
        public List<int> bookId { get; set; }
    }
}
