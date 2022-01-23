using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interop.Data
{
    public class OwnershipDto
    {
        public int Id { get; set; }

        public int SubscriptionId { get; set; }
        public SubscriptionDto Subscription { get; set; }

        public int BookId { get; set; }
        public BookCopyDto Book { get; set; }

        public DateTime AcquisitionDate { get; set; }

        public DateTime PromisedReturnDate { get; set; }

        public DateTime? ReturnDate { get; set; }
    }
}
