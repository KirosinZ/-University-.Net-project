using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interop.Data
{
    public class SubscriptionDto
    {
        public int Id { get; set; }

        public int OwnerId { get; set; }
        public ReaderDto Owner { get; set; }

        public int TypeId { get; set; }
        public LiteratureTypeDto Type { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public IEnumerable<OwnershipDto> Ownerships { get; set; }
    }
}
