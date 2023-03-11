using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
	public class UserRewards
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public virtual User User { get; set; }
		public int RewardId { get; set; }
		public virtual Reward Reward { get; set; }
		public int Progress { get; set; }
	}
}
