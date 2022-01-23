using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Business.Interop.Data;

namespace Business.Services
{
    public interface ISubscriptionService
    {
        public SubscriptionDto CreateSubscription(SubscriptionDto Subscription);
        public SubscriptionDto UpdateSubscription(SubscriptionDto newSubscription);
        public SubscriptionDto CreateOrUpdateSubscription(SubscriptionDto Subscription);

        public void DeleteSubscription(int index);

        public IEnumerable<SubscriptionDto> GetAll();

        public IEnumerable<SubscriptionDto> FindByReaderId(int index);
        public IEnumerable<SubscriptionDto> FindByTypeId(int index);
    }
}
