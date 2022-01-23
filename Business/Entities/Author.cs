using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class Author
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public IEnumerable<Book> Books { get; set; }
    }
}
