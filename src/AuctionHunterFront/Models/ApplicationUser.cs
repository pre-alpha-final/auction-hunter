using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace AuctionHunterFront.Models
{
	public class ApplicationUser : IdentityUser
	{
		public virtual ICollection<ApplicationUserAuctionHunterItem> ApplicationUserAuctionHunterItems { get; set; }

		public ApplicationUser()
		{
			ApplicationUserAuctionHunterItems = new HashSet<ApplicationUserAuctionHunterItem>();
		}
	}
}
