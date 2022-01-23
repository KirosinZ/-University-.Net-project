using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Business.Interop.Data;

namespace Business.Services
{
    public interface IAuthorService
    {
        public AuthorDto CreateAuthor(AuthorDto author);
        public AuthorDto UpdateAuthor(AuthorDto newauthor);
        public AuthorDto CreateOrUpdateAuthor(AuthorDto author);

        public void DeleteAuthor(int index);

        public IEnumerable<AuthorDto> GetAll();

        public IEnumerable<AuthorDto> FindByName(string name);
        public IEnumerable<AuthorDto> FindByCountryId(int index);
    }
}
