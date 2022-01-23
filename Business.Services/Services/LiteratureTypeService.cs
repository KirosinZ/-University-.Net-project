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
    public class LiteratureTypeService : ILiteratureTypeService
    {
        private readonly ILiteratureTypeRepository _literatureTypeRepository;
        private readonly IMapper _mapper;

        public LiteratureTypeService(ILiteratureTypeRepository literatureTypeRepository, IMapper mapper)
        {
            _literatureTypeRepository = literatureTypeRepository;
            _mapper = mapper;
        }

        public IEnumerable<LiteratureTypeDto> GetAll()
        {
            return _mapper.Map<IEnumerable<LiteratureType>, IEnumerable<LiteratureTypeDto>>(_literatureTypeRepository.Query());
        }
    }
}
