using Microsoft.EntityFrameworkCore;

namespace AuctionHunterFront.Models
{
	public class AuctionHunterDbContext : DbContext
	{
		public AuctionHunterDbContext(DbContextOptions<AuctionHunterDbContext> options)
		: base(options)
		{
		}

		public DbSet<AuctionHunterItem> AuctionHunterItems { get; set; }
	}
}
