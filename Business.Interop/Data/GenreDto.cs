using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interop.Data
{
    public class GenreDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TypeId { get; set; }
        public LiteratureTypeDto Type { get; set; }
    }
}
