using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class Subscription
    {
        public int Id { get; set; }

        public int OwnerId { get; set; }
        public Reader Owner { get; set; }

        public int TypeId { get; set; }
        public LiteratureType Type { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public IEnumerable<Ownership> Ownerships { get; set; }
    }
}
