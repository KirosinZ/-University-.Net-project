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
    public class OwnershipRepository : AbstractRepository<Ownership, int>, IOwnershipRepository
    {
        public OwnershipRepository(Context context)
        {
            _context = context;
        }

        protected override int KeySelector(Ownership value)
        {
            return value.Id;
        }

        protected override IQueryable<Ownership> QueryImplementation()
        {
            return _context.Ownerships
                .Include(o => o.Subscription)
                    .ThenInclude(s => s.Owner)
                .Include(o => o.Subscription)
                    .ThenInclude(s => s.Type)
                .Include(o => o.Book)
                    .ThenInclude(bc => bc.Language)
                .Include(o => o.Book)
                    .ThenInclude(bc => bc.Book)
                        .ThenInclude(b => b.Author)
                            .ThenInclude(a => a.Country)
                .Include(o => o.Book)
                    .ThenInclude(bc => bc.Book)
                        .ThenInclude(b => b.Genre)
                            .ThenInclude(g => g.Type)
                .Include(o => o.Book)
                    .ThenInclude(bc => bc.Book)
                        .ThenInclude(b => b.OriginalLanguage);

        }
    }
}
