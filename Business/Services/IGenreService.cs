using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Business.Interop.Data;

namespace Business.Services
{
    public interface IGenreService
    {
        public IEnumerable<GenreDto> GetAll();

        public IEnumerable<GenreDto> FindByTypeId(int index);
    }
}
