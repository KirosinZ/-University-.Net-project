using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class Reader
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public int Balance { get; set; }

        public IEnumerable<Subscription> Subscriptions { get; set; }
    }
}
