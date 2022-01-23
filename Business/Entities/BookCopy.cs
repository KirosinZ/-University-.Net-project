using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class BookCopy
    {
        public int Id { get; set; }

        public bool Available { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }

        public IEnumerable<Ownership> Ownerships { get; set; }
    }
}
