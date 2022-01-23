using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Business.Interop.Data;

namespace ASPTEST.Models
{
    public class DepositViewModel
    {
        public int ReaderId { get; set; }
        public ReaderDto Reader { get; set; }
        public int DepositAmount { get; set; }
    }
}
