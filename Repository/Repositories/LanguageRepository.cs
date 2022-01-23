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
    public class LanguageRepository : AbstractRepository<Language, int>, ILanguageRepository
    {
        public LanguageRepository(Context context)
        {
            _context = context;
        }

        protected override int KeySelector(Language value)
        {
            return value.Id;
        }

        protected override IQueryable<Language> QueryImplementation()
        {
            return _context.Languages;
        }
    }
}
