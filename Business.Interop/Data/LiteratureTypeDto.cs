using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interop.Data
{
    public class LiteratureTypeDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public IEnumerable<GenreDto> Genres { get; set; }
    }
}
