using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Business.Interop.Data;

namespace ASPTEST.Models
{
    public class BookBrowserViewModel
    {
        public int ReaderId { get; set; }
        //public IEnumerable<(BookCopyDto Copy, BookDto BookInfo)> Books { get; set; }
        public IEnumerable<IEnumerable<(BookCopyDto Copy, BookDto BookInfo)>> Books { get; set; }
        public IEnumerable<SubscriptionDto> ActiveSubs { get; set; }
    }
}
