using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class User : IdentityUser
    {
        public virtual int? Cash { get; set; }
        public virtual int? HighestBet { get; set; }
        public virtual int? HighestWin { get; set; }
        public virtual int? TotalBetsCount { get; set; }

        public void Deffault()
        {
            Cash = 0;
			HighestBet = 0;
			HighestWin = 0;
			TotalBetsCount = 0;
		}
	}
}
