using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.VM
{
    public class UserVM
    {
        public string UserName { get; set; } = string.Empty;
        public int Cash { get; set; }
        public int HighestBet { get; set; }
        public int HighestWin { get; set; }
        public int TotalBetsCount { get; set; }
	}
}
