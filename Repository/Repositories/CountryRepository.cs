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
    public class CountryRepository : AbstractRepository<Country, int>, ICountryRepository
    {
        public CountryRepository(Context context)
        {
            _context = context;
        }

        protected override int KeySelector(Country value)
        {
            return value.Id;
        }

        protected override IQueryable<Country> QueryImplementation()
        {
            return _context.Countries;
        }
    }
}
