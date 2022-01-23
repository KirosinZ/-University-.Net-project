using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Business.Interop.Data;

namespace Business.Services
{
    public interface IBookCopyService
    {
        public BookCopyDto CreateBookCopy(BookCopyDto bookcopy);
        public BookCopyDto UpdateBookCopy(BookCopyDto newbookcopy);
        public BookCopyDto CreateOrUpdateBookCopy(BookCopyDto bookcopy);

        public void DeleteBookCopy(int index);

        public IEnumerable<BookCopyDto> GetAll();

        public IEnumerable<BookCopyDto> FindByBookId(int index);
    }
}
