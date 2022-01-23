using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class Ownership
    {
        public int Id { get; set; }

        public int SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }

        public int BookId { get; set; }
        public BookCopy Book { get; set; }

        public DateTime AcquisitionDate { get; set; }

        public DateTime PromisedReturnDate { get; set; }

        public DateTime? ReturnDate { get; set; }
    }
}
