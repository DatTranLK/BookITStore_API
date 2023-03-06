using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.DetailComboBookDTO
{
    public class DetailComboBookDtoShow
    {
        public int Id { get; set; }
        public int? BookId { get; set; }
        public int? ComboBookId { get; set; }
        public string ComboName { get; set; }
        public string BookName { get; set; }
        public string BookIsbn { get; set; }
        public string BookAuthor { get; set; }
        public string CategoryName { get; set; }
        public string PublisherName { get; set; }
    }
}
