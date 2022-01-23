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
    public class ReaderRepository : AbstractRepository<Reader, int>, IReaderRepository
    {
        public ReaderRepository(Context context)
        {
            _context = context;
        }

        protected override int KeySelector(Reader value)
        {
            return value.Id;
        }

        protected override IQueryable<Reader> QueryImplementation()
        {
            return _context.Readers
                .Include(r => r.Subscriptions)
                    .ThenInclude(s => s.Ownerships);
        }
    }
}
