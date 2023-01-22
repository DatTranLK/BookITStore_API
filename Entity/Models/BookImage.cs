using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class BookImage
    {
        public int Id { get; set; }
        public string ImgPath { get; set; }
        public int? BookId { get; set; }

        public virtual Book Book { get; set; }
    }
}
