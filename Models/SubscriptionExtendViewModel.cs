using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Business.Interop.Data;

namespace ASPTEST.Models
{
    public class SubscriptionExtendViewModel
    {
        public SubscriptionDto Subscription { get; set; }
        public int Extension { get; set; }
    }
}
