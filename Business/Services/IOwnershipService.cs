using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Business.Interop.Data;

namespace Business.Services
{
    public interface IOwnershipService
    {
        public OwnershipDto CreateOwnership(OwnershipDto ownership);
        public OwnershipDto UpdateOwnership(OwnershipDto newownership);
        public OwnershipDto CreateOrUpdateOwnership(OwnershipDto ownership);

        public void DeleteOwnership(int index);

        public IEnumerable<OwnershipDto> GetAll();

        public IEnumerable<OwnershipDto> FindBySubscriptionId(int index);
        public IEnumerable<OwnershipDto> FindByBookCopyId(int index);
    }
}
