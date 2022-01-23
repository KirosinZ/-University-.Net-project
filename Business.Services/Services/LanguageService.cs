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
    public class LanguageService : ILanguageService
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IMapper _mapper;

        public LanguageService(ILanguageRepository languageRepository, IMapper mapper)
        {
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public IEnumerable<LanguageDto> GetAll()
        {
            return _mapper.Map<IEnumerable<Language>, IEnumerable<LanguageDto>>(_languageRepository.Query());
        }
    }
}
