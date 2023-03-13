using Entity.Dtos.EBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entity.Dtos.DetailComboBookDTO
{
    public class ListEBookOfCombo
    {
        public int EbookId { get; set; }
        public int? Price { get; set; }
        public string PdfUrl { get; set; }

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
        public int? CategoryName { get; set; }
        public int? PublisherName { get; set; }
    }
}
