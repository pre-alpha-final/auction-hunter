using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AuctionHunterFront.Models
{
	public class AuctionHunterDbContext : IdentityDbContext<ApplicationUser>
	{
		private readonly IConfiguration _configuration;

		public DbSet<AuctionHunterItem> AuctionHunterItems { get; set; }

		public AuctionHunterDbContext(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			optionsBuilder.UseMySql(_configuration.GetConnectionString("AuctionHunterDbContext"));
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<AuctionHunterItem>()
				.HasIndex(u => u.AuctionLink)
				.IsUnique();
		}
	}
}
