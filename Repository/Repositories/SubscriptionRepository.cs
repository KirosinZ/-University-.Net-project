using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Business.Entities;
using Business.Repositories.DataRepositories;

using Repository.Data;

namespace Repository.Repositories
{
    public class SubscriptionRepository : AbstractRepository<Subscription, int>, ISubscriptionRepository
    {
        public SubscriptionRepository(Context context)
        {
            _context = context;
        }

        protected override int KeySelector(Subscription value)
        {
            return value.Id;
        }

        protected override IQueryable<Subscription> QueryImplementation()
        {
            return _context.Subscriptions
                .Include(s => s.Owner)
                .Include(s => s.Type)
                .Include(s => s.Ownerships);
        }
    }
}
