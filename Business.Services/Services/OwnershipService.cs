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
    public class OwnershipService : IOwnershipService
    {
        private readonly IOwnershipRepository _ownershipRepository;
        private readonly IMapper _mapper;

        public OwnershipService(IOwnershipRepository ownershipRepository, IMapper mapper)
        {
            _ownershipRepository = ownershipRepository;
            _mapper = mapper;
        }

        public OwnershipDto CreateOwnership(OwnershipDto ownership)
        {
            var entity = _mapper.Map<Ownership>(ownership);

            _ownershipRepository.Create(entity);
            return _mapper.Map<OwnershipDto>(entity);
        }

        public OwnershipDto CreateOrUpdateOwnership(OwnershipDto ownership)
        {
            var entity = _mapper.Map<Ownership>(ownership);

            _ownershipRepository.CreateOrUpdate(entity);
            return _mapper.Map<OwnershipDto>(entity);
        }

        public OwnershipDto UpdateOwnership(OwnershipDto newownership)
        {
            var entity = _mapper.Map<Ownership>(newownership);

            _ownershipRepository.Update(entity);
            return _mapper.Map<OwnershipDto>(entity);
        }

        public void DeleteOwnership(int index)
        {
            _ownershipRepository.Delete(_ownershipRepository.Query().Where(o => o.Id == index));
        }

        public IEnumerable<OwnershipDto> FindByBookCopyId(int index)
        {
            return _mapper.Map<IEnumerable<Ownership>, IEnumerable<OwnershipDto>>(_ownershipRepository.Query().Where(o => o.Book.Id == index));
        }

        public IEnumerable<OwnershipDto> FindBySubscriptionId(int index)
        {
            return _mapper.Map<IEnumerable<Ownership>, IEnumerable<OwnershipDto>>(_ownershipRepository.Query().Where(o => o.Subscription.Id == index));
        }

        public IEnumerable<OwnershipDto> GetAll()
        {
            return _mapper.Map<IEnumerable<Ownership>, IEnumerable<OwnershipDto>>(_ownershipRepository.Query());
        }
    }
}
