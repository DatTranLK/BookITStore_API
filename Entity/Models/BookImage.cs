using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Entity.Models
{
    public partial class BookImage
    {
        public int Id { get; set; }
        public string ImgPath { get; set; }
        public int? BookId { get; set; }
        public int? EbookId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Book Book { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Ebook Ebook { get; set; }
    }
}
