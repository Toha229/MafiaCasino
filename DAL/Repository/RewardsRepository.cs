using AutoMapper;
using DAL.Models;
using DAL.Models.VM;
using DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
	public class RewardsRepository : IRewardsRopository
	{
		private readonly IMapper _mapper;
		public RewardsRepository(IMapper mapper)
		{
			_mapper = mapper;
		}
		public async Task<List<RewardsVM>?> GetRewardsAsync(string userId)
		{
			using (var _context = new AppDbContext())
			{
				var urewards = await _context.Rewards
				.GroupJoin(_context.UserRewards.Where(r => r.UserId == userId),
					r => r.Id,
					ur => ur.RewardId,
					(r, ur) => new { Reward = r, UserReward = ur.FirstOrDefault() }).ToListAsync();

				List<RewardsVM> rewards = new List<RewardsVM>();
				foreach (var r in urewards)
				{
					RewardsVM mappedReward = _mapper.Map<Reward, RewardsVM>(r.Reward);
					if (r.UserReward != null)
					{
						mappedReward.Progress = r.UserReward.Progress;
					}
					else
					{
						mappedReward.Progress = 0;
					}
					rewards.Add(mappedReward);
				}
				return rewards;
			}
		}
		public async Task SetRewardProgress(string userId, string rewardName, int progress)
		{
			using (var _context = new AppDbContext())
			{
				var reward = await GetRewardByName(rewardName);
				var ureward = await _context.UserRewards.Where(r => r.UserId == userId).FirstOrDefaultAsync();
				if(ureward == null)
				{
					ureward = new UserRewards() { RewardId = reward.Id, UserId = userId, Progress = progress };
					await _context.AddAsync(ureward);
					await _context.SaveChangesAsync();
					return;
				}
				ureward.Progress = progress;
				await _context.SaveChangesAsync();
			}
		}
		public async Task AddRewardProgress(string userId, string rewardName, int progress)
		{
			using (var _context = new AppDbContext())
			{
				var reward = await GetRewardByName(rewardName);
				var ureward = await _context.UserRewards.Where(r => r.UserId == userId).FirstOrDefaultAsync();
				ureward.Progress += progress;
				await _context.SaveChangesAsync();
			}
		}
		public async Task<Reward?> GetRewardByName(string rewardName)
		{
			using (var _context = new AppDbContext())
			{
				var reward = await _context.Rewards.Where(r => r.Name.ToLower() == rewardName.ToLower()).FirstOrDefaultAsync();
				return reward;
			}
		}
	}
}
