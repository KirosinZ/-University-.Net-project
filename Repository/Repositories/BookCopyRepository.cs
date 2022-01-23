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
    public class BookCopyRepository : AbstractRepository<BookCopy, int>, IBookCopyRepository
    {
        public BookCopyRepository(Context context)
        {
            _context = context;
        }

        protected override int KeySelector(BookCopy value)
        {
            return value.Id;
        }

        protected override IQueryable<BookCopy> QueryImplementation()
        {
            return _context.BookCopies
                .Include(bc => bc.Language)
                .Include(bc => bc.Book)
                    .ThenInclude(b => b.Genre)
                        .ThenInclude(g => g.Type)
                .Include(bc => bc.Book)
                    .ThenInclude(b => b.OriginalLanguage)
                .Include(bc => bc.Book)
                    .ThenInclude(b => b.Author)
                        .ThenInclude(a => a.Country);
        }
    }
}
