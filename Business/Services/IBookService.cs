using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Business.Interop.Data;

namespace Business.Services
{
    public interface IBookService
    {
        public BookDto CreateBook(BookDto book);
        public BookDto UpdateBook(BookDto newbook);
        public BookDto CreateOrUpdateBook(BookDto book);

        public void DeleteBook(int index);

        public IEnumerable<BookDto> GetAll();

        public IEnumerable<BookDto> FindByTitle(string title);
        public IEnumerable<BookDto> FindByAuthorId(int index);
        public IEnumerable<BookDto> FindByLanguageId(int index);
    }
}
