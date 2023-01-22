using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class DetailComboBook
    {
        public int Id { get; set; }
        public int? BookId { get; set; }
        public int? ComboBookId { get; set; }

        public virtual Book Book { get; set; }
        public virtual ComboBook ComboBook { get; set; }
    }
}
