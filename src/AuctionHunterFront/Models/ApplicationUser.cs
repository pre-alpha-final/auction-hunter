using System;
using Microsoft.AspNetCore.Identity;

namespace AuctionHunterFront.Models
{
	public class ApplicationUser : IdentityUser
	{
		public Guid OriginProductId { get; set; } = new Guid("1d2f5fda-e58e-43ee-a8a1-88001a6262ad"); // AuctionHunter
	}
}
