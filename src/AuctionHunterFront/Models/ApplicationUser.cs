using System;
using Microsoft.AspNetCore.Identity;

namespace AuctionHunterFront.Models
{
	public class ApplicationUser : IdentityUser
	{
		public Guid OriginProductId { get; set; }
	}
}
