using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interop.Data
{
    public class BookCopyDto
    {
        public int Id { get; set; }

        public bool Available { get; set; }

        public int BookId { get; set; }
        public BookDto Book { get; set; }

        public int LanguageId { get; set; }
        public LanguageDto Language { get; set; }

        public IEnumerable<OwnershipDto> Ownerships { get; set; }
    }
}
