using DAL.Models;
using DAL.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.IRepository
{
	public interface IRewardsRopository
	{
		Task<List<RewardsVM>?> GetRewardsAsync(string userId);
		Task SetRewardProgress(string userId, string rewardName, int progress);
		Task AddRewardProgress(string userId, string rewardName, int progress);
		Task<Reward?> GetRewardByName(string rewardName);
	}
}
