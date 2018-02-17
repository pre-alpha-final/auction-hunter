using Microsoft.EntityFrameworkCore;

namespace AuctionHunterFront.Models
{
	public class AuctionHunterContext : DbContext
	{
		public AuctionHunterContext(DbContextOptions<AuctionHunterContext> options)
		: base(options)
		{
		}

		public DbSet<AuctionHunterItem> AuctionHunterItems { get; set; }
	}
}
