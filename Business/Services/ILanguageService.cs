using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Business.Interop.Data;

namespace Business.Services
{
    public interface ILanguageService
    {
        public IEnumerable<LanguageDto> GetAll();
    }
}
