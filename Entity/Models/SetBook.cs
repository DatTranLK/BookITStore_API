﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class SetBook
    {
        public SetBook()
        {
            Books = new HashSet<Book>();
        }

        public int SetBookId { get; set; }
        public string Name { get; set; }
        public int? Indexes { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
