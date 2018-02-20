using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuctionHunterFront.Models
{
	public class AuctionHunterDbContext : IdentityDbContext<ApplicationUser>
	{
		public AuctionHunterDbContext(DbContextOptions<AuctionHunterDbContext> options)
		: base(options)
		{
		}

		public DbSet<AuctionHunterItem> AuctionHunterItems { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<AuctionHunterItem>()
				.HasIndex(u => u.AuctionLink)
				.IsUnique();
		}
	}
}
