using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interop.Data
{
    public class AuthorDto
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public int CountryId { get; set; }
        public CountryDto Country { get; set; }

        public IEnumerable<BookDto> Books { get; set; }
    }
}
