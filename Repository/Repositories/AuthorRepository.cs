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
    public class AuthorRepository : AbstractRepository<Author, int>, IAuthorRepository
    {
        public AuthorRepository(Context context)
        {
            _context = context;
        }

        protected override int KeySelector(Author value)
        {
            return value.Id;
        }

        protected override IQueryable<Author> QueryImplementation()
        {
            return _context.Authors
                .Include(a => a.Country)
                .Include(a => a.Books);
        }
    }
}
