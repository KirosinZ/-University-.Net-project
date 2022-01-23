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
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public IEnumerable<GenreDto> FindByTypeId(int index)
        {
            return _mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDto>>(_genreRepository.Query().Where(g => g.Type.Id == index));
        }

        public IEnumerable<GenreDto> GetAll()
        {
            return _mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDto>>(_genreRepository.Query());
        }
    }
}
