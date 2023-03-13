using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.DetailComboBookDTO
{
    public class DetailComboEBookDtoShow
    {
        public int Id { get; set; }
        public int? EBookId { get; set; }
        public int? ComboBookId { get; set; }
        public string ComboName { get; set; }
        public string EBookName { get; set; }
        public string EBookIsbn { get; set; }
        public string EBookAuthor { get; set; }
        public string CategoryName { get; set; }
        public string PublisherName { get; set; }
    }
}
