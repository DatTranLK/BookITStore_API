using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.BookImage
{
    public class BookImageDto
    {
        public int Id { get; set; }
        public string ImgPath { get; set; }
        public int? BookId { get; set; }

        public string? BookName { get; set; }
    }
}
