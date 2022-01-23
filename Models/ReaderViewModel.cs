using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Business.Interop.Data;

namespace ASPTEST.Models
{
    public class ReaderViewModel
    {
        public int ReaderId { get; set; }
        public ReaderDto Reader { get; set; }
        public IEnumerable<(LiteratureTypeDto Type, SubscriptionDto Sub)> SubData { get; set; }
        public IEnumerable<OwnershipDto> BookData { get; set; }
    }
}
