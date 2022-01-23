using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Business.Entities;
using Business.Interop.Data;
using Business.Repositories.DataRepositories;
using AutoMapper;

namespace Business.Services.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public AuthorDto CreateAuthor(AuthorDto author)
        {
            var entity = _mapper.Map<Author>(author);

            _authorRepository.Create(entity);
            return _mapper.Map<AuthorDto>(entity);
        }

        public AuthorDto CreateOrUpdateAuthor(AuthorDto author)
        {
            var entity = _mapper.Map<Author>(author);

            _authorRepository.CreateOrUpdate(entity);
            return _mapper.Map<AuthorDto>(entity);
        }

        public AuthorDto UpdateAuthor(AuthorDto newauthor)
        {
            var entity = _mapper.Map<Author>(newauthor);

            _authorRepository.Update(entity);
            return _mapper.Map<AuthorDto>(entity);
        }

        public void DeleteAuthor(int index)
        {
            _authorRepository.Delete(_authorRepository.Query().Where(a => a.Id == index));
        }

        public IEnumerable<AuthorDto> FindByCountryId(int index)
        {
            return _mapper.Map<IEnumerable<Author>, IEnumerable<AuthorDto>>(_authorRepository.Query().Where(a => a.Country.Id == index));
        }

        public IEnumerable<AuthorDto> FindByName(string name)
        {
            return _mapper.Map<IEnumerable<Author>, IEnumerable<AuthorDto>>(_authorRepository.Query().Where(a => string.Compare(name, a.FullName, StringComparison.InvariantCultureIgnoreCase) == 0));
        }

        public IEnumerable<AuthorDto> GetAll()
        {
            return _mapper.Map<IEnumerable<Author>, IEnumerable<AuthorDto>>(_authorRepository.Query());
        }
    }
}
