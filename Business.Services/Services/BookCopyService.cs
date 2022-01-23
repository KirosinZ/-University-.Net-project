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
    public class BookCopyService : IBookCopyService
    {
        private readonly IBookCopyRepository _bookCopyRepository;
        private readonly IMapper _mapper;

        public BookCopyService(IBookCopyRepository bookCopyRepository, IMapper mapper)
        {
            _bookCopyRepository = bookCopyRepository;
            _mapper = mapper;
        }

        public BookCopyDto CreateBookCopy(BookCopyDto bookcopy)
        {
            var entity = _mapper.Map<BookCopy>(bookcopy);

            _bookCopyRepository.Create(entity);
            return _mapper.Map<BookCopyDto>(entity);
        }

        public BookCopyDto CreateOrUpdateBookCopy(BookCopyDto bookcopy)
        {
            var entity = _mapper.Map<BookCopy>(bookcopy);

            _bookCopyRepository.CreateOrUpdate(entity);
            return _mapper.Map<BookCopyDto>(entity);
        }

        public BookCopyDto UpdateBookCopy(BookCopyDto newbookcopy)
        {
            var entity = _mapper.Map<BookCopy>(newbookcopy);

            _bookCopyRepository.Update(entity);
            return _mapper.Map<BookCopyDto>(entity);
        }

        public void DeleteBookCopy(int index)
        {
            _bookCopyRepository.Delete(_bookCopyRepository.Query().Where(bc => bc.Id == index));
        }

        public IEnumerable<BookCopyDto> FindByBookId(int index)
        {
            return _mapper.Map<IEnumerable<BookCopy>, IEnumerable<BookCopyDto>>(_bookCopyRepository.Query().Where(bc => bc.Book.Id == index));
        }

        public IEnumerable<BookCopyDto> GetAll()
        {
            return _mapper.Map<IEnumerable<BookCopy>, IEnumerable<BookCopyDto>>(_bookCopyRepository.Query());
        }
    }
}
