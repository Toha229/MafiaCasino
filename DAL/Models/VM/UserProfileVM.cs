using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.VM
{
	public class UserProfileVM
	{
		public string UserName { get; set; } = string.Empty;
		public string Avatar { get; set;  } = string.Empty;
		public int Cash { get; set; }
		public int HighestBet { get; set; }
		public int HighestWin { get; set; }
		public int TotalBetsCount { get; set; }
		public List<RewardsVM>? Rewards { get; set; }
	}
}
