using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.VM
{
	public class RewardsVM
	{
		public string Name { get; set; } = string.Empty;
		public virtual string? Description { get; set; } = string.Empty;
		public int Target { get; set; }
		public int Progress { get; set; }
	}
}
