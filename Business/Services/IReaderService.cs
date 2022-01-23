using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Business.Interop.Data;

namespace Business.Services
{
    public interface IReaderService
    {
        public ReaderDto CreateReader(ReaderDto reader);
        public ReaderDto UpdateReader(ReaderDto reader);
        public ReaderDto CreateOrUpdateReader(ReaderDto reader);

        public void DeleteReader(int index);

        public IEnumerable<ReaderDto> GetAll();

        public IEnumerable<ReaderDto> FindByFullName(string name);
    }
}
