using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entity.Dtos.EBook
{
    public class EBookDtoForUpdate
    {
        public int? Price { get; set; }
        public string PdfUrl { get; set; }

    }
}
