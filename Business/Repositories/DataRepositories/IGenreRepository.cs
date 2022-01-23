using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Business.Entities;

namespace Business.Repositories.DataRepositories
{
    public interface IGenreRepository : IRepository<Genre, int> { }
}
