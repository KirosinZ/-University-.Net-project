using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Business.Interop.Data;

namespace ASPTEST.Models
{
    public class OwnershipExtendViewModel
    {
        public OwnershipDto Ownership { get; set; }
        public int MaxExtension { get; set; }
        public int Extension { get; set; }
    }
}
