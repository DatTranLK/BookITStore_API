﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entity.Dtos.EBook
{
    public class EBookDto
    {
        public int BookId { get; set; }
        public int EbookId { get; set; }
        public int? Price { get; set; }
        public string PdfUrl { get; set; }
        public bool? HasPhysicalBook { get; set; }

        public string Name { get; set; }
        public string Isbn { get; set; }
        public string Author { get; set; }
        public string ReleaseYear { get; set; }
        public int? Version { get; set; }
        public string Description { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? AmountSold { get; set; }
        public bool? IsActive { get; set; }
        public int? CategoryId { get; set; }
        public int? PublisherId { get; set; }
        public int? SetBookId { get; set; }
        public bool? IsSetBook { get; set; }
    }
}
