﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.Book
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Isbn { get; set; }
        public string Author { get; set; }
        public string ReleaseYear { get; set; }
        public int? Version { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public int? Amount { get; set; }

        public string CategoryName { get; set; }
        public string PublisherName { get; set; }
    }
}
