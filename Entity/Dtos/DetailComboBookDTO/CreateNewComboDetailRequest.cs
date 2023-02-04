using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.DetailComboBookDTO
{
    public class CreateNewComboDetailRequest
    {
        public int comboId { get; set; }
        public List<int> bookId { get; set; }

    }
}
