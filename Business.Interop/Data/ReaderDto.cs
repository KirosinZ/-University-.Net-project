using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interop.Data
{
    public class ReaderDto
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public int Balance { get; set; }

        public IEnumerable<SubscriptionDto> Subscriptions { get; set; }
    }
}
