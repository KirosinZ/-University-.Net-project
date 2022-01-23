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
    public class LiteratureTypeRepository : AbstractRepository<LiteratureType, int>, ILiteratureTypeRepository
    {
        public LiteratureTypeRepository(Context context)
        {
            _context = context;
        }

        protected override int KeySelector(LiteratureType value)
        {
            return value.Id;
        }

        protected override IQueryable<LiteratureType> QueryImplementation()
        {
            return _context.LiteratureTypes
                .Include(lt => lt.Genres);
        }
    }
}
