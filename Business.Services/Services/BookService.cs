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
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public BookDto CreateBook(BookDto book)
        {
            var entity = _mapper.Map<Book>(book);

            _bookRepository.Create(entity);
            return _mapper.Map<BookDto>(entity);
        }

        public BookDto CreateOrUpdateBook(BookDto book)
        {
            var entity = _mapper.Map<Book>(book);

            _bookRepository.CreateOrUpdate(entity);
            return _mapper.Map<BookDto>(entity);
        }

        public BookDto UpdateBook(BookDto newbook)
        {
            var entity = _mapper.Map<Book>(newbook);

            _bookRepository.Update(entity);
            return _mapper.Map<BookDto>(entity);
        }

        public void DeleteBook(int index)
        {
            _bookRepository.Delete(_bookRepository.Query().Where(b => b.Id == index));
        }

        public IEnumerable<BookDto> FindByAuthorId(int index)
        {
            return _mapper.Map<IEnumerable<Book>, IEnumerable<BookDto>>(_bookRepository.Query().Where(b => b.Author.Id == index));
        }

        public IEnumerable<BookDto> FindByLanguageId(int index)
        {
            return _mapper.Map<IEnumerable<Book>, IEnumerable<BookDto>>(_bookRepository.Query().Where(b => b.OriginalLanguage.Id == index));
        }

        public IEnumerable<BookDto> FindByTitle(string title)
        {
            return _mapper.Map<IEnumerable<Book>, IEnumerable<BookDto>>(_bookRepository.Query().Where(b => 0 == string.Compare(title, b.Title, StringComparison.InvariantCultureIgnoreCase)));
        }

        public IEnumerable<BookDto> GetAll()
        {
            return _mapper.Map<IEnumerable<Book>, IEnumerable<BookDto>>(_bookRepository.Query());
        }
    }
}
