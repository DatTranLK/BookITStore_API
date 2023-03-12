using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Entity.Models
{
    public partial class Book
    {
        public Book()
        {
            BookImages = new HashSet<BookImage>();
            DetailComboBooks = new HashSet<DetailComboBook>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Isbn { get; set; }
        public string Author { get; set; }
        public string ReleaseYear { get; set; }
        public int? Version { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public int? Amount { get; set; }
        public int? AmountSold { get; set; }
        public bool? IsActive { get; set; }
        public int? CategoryId { get; set; }
        public int? PublisherId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Category Category { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Publisher Publisher { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual ICollection<BookImage> BookImages { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual ICollection<DetailComboBook> DetailComboBooks { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
