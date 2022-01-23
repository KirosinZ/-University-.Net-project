using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Business.Entities;
using Business.Interop.Data;
using Business.Repositories.DataRepositories;
using AutoMapper;

namespace Business.Services.Services
{
    public class ReaderService : IReaderService
    {
        private readonly IReaderRepository _readerRepository;
        private readonly IMapper _mapper;

        public ReaderService(IReaderRepository readerRepository, IMapper mapper)
        {
            _readerRepository = readerRepository;
            _mapper = mapper;
        }

        public ReaderDto CreateReader(ReaderDto reader)
        {
            var entity = _mapper.Map<Reader>(reader);

            _readerRepository.Create(entity);
            return _mapper.Map<ReaderDto>(entity);
        }

        public ReaderDto CreateOrUpdateReader(ReaderDto reader)
        {
            var entity = _mapper.Map<Reader>(reader);

            _readerRepository.CreateOrUpdate(entity);
            return _mapper.Map<ReaderDto>(entity);
        }

        public ReaderDto UpdateReader(ReaderDto reader)
        {
            var entity = _mapper.Map<Reader>(reader);

            _readerRepository.Update(entity);
            return _mapper.Map<ReaderDto>(entity);
        }

        public void DeleteReader(int index)
        {
            _readerRepository.Delete(_readerRepository.Query().Where(r => r.Id == index));
        }

        public IEnumerable<ReaderDto> FindByFullName(string name)
        {
            return _mapper.Map<IEnumerable<Reader>, IEnumerable<ReaderDto>>(_readerRepository.Query().Where(r => 0 == string.Compare(r.FullName, name, StringComparison.InvariantCultureIgnoreCase)));
        }

        public IEnumerable<ReaderDto> GetAll()
        {
            return _mapper.Map<IEnumerable<Reader>, IEnumerable<ReaderDto>>(_readerRepository.Query());
        }
    }
}
