using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Enum
{
    public enum OrderStatus
    {
        In_Progress,
        Accepted,
        Paid,
        Physical_book_delivered,
        Ebook_delivered,
        Done,
        Cancel
    }
}
