using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Business.Interop.Data;

namespace ASPTEST.Models
{
    public class SubscriptionIssueViewModel
    {
        public ReaderDto Reader { get; set; }
        public LiteratureTypeDto Type { get; set; }
        public DateTime AcquisitionDate { get; set; }
        public int Period { get; set; }
    }
}
