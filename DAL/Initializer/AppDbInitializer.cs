using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Initializer
{
	public class AppDbInitializer
	{
		public static void Seed()
		{
			using(var _context = new AppDbContext())
			{
				if (_context.Rewards.Count() == 0)
				{
					_context.AddRange(InitRewards());
				}
				_context.SaveChanges();
			}
		}
		public static List<Reward> InitRewards()
		{
			List<Reward> rewards = new List<Reward>()
			{
				new Reward(){ Name="Civilian", Description="Become the mobser, create account!", Target=1},

				new Reward(){ Name="Associate", Description="Donate first time", Target=1},
				new Reward(){ Name="Soldier", Description="Donate any cash 5 times", Target=5},
				new Reward(){ Name="Caporegime", Description="Donate 1.000 UAH in one time", Target=1},
				new Reward(){ Name="Consigliere", Description="Donate 15 times", Target=25},
				new Reward(){ Name="Underboss", Description="Donate summary 10.000 UAH at all", Target=10000},
				new Reward(){ Name="Boss", Description="Donate 100.000 UAH in our project for all time", Target=100000},

				new Reward(){ Name="The thing is in the hat", Description="Collect all the hats", Target=1},
				new Reward(){ Name="Here is the mobster", Description="Collect the mobster 5 times", Target=5},
				new Reward(){ Name="Directly hitting the target", Description="Shoot the beer 50 times", Target=50}
			};
			return rewards;
		}
	}
}
