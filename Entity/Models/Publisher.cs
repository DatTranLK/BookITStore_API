using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class Publisher
    {
        public Publisher()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
