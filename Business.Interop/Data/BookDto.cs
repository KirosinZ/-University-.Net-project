using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interop.Data
{
    public class BookDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int NumberOfPages { get; set; }

        public int Price { get; set; }

        public int AuthorId { get; set; }
        public AuthorDto Author { get; set; }

        public int OriginalLanguageId { get; set; }
        public LanguageDto OriginalLanguage { get; set; }

        public int GenreId { get; set; }
        public GenreDto Genre { get; set; }

        public IEnumerable<BookCopyDto> Copies { get; set; }
    }
}
