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
    public class BookRepository : AbstractRepository<Book, int>, IBookRepository
    {
        public BookRepository(Context context)
        {
            _context = context;
        }

        protected override int KeySelector(Book value)
        {
            return value.Id;
        }

        protected override IQueryable<Book> QueryImplementation()
        {
            return _context.Books
                .Include(b => b.Genre)
                    .ThenInclude(g => g.Type)
                .Include(b => b.OriginalLanguage)
                .Include(b => b.Author)
                    .ThenInclude(a => a.Country);
        }
    }
}
