using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Business.Interop.Data;

namespace ASPTEST.Models
{
    public class BookTakeViewModel
    {
        public ReaderDto Reader { get; set; }
        public SubscriptionDto Sub { get; set; }
        public BookCopyDto Copy { get; set; }
        public int MaxPeriod { get; set; }
        public int Period { get; set; }
    }
}
