using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entity.Dtos.Book
{
    public class BookDtoForPhysicalAndEBook
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
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? AmountSold { get; set; }
        public int? CategoryId { get; set; }
        public int? PublisherId { get; set; }
        public int? EBookPrice { get; set; }
        public string PdfUrl { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? HasPhysicalBook { get; set; }
    }
}
