using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Business.Interop.Data;

namespace ASPTEST.Models
{
    public class BookReturnViewModel
    {
        public ReaderDto Reader { get; set; }
        public BookDto Book { get; set; }
        public int Delay { get; set; }
        public int Fee { get; set; }
    }
}
