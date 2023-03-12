using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Entity.Models
{
    public partial class DetailComboBook
    {
        public int Id { get; set; }
        public int? BookId { get; set; }
        public int? ComboBookId { get; set; }
        public int? EbookId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Book Book { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual ComboBook ComboBook { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Ebook Ebook { get; set; }
    }
}
