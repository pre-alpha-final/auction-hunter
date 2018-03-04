using AuctionHunter.Infrastructure;
using AuctionHunter.Results;
using AuctionHunterFront.Models;
using System.Threading.Tasks;

namespace AuctionHunterFront.Services
{
	public interface IAuctionHunterService
	{
		Task Start();
		Task<PageResult> GetItems(int pageNumber);
		Task TryAddAsync(AuctionHunterDbContext auctionHunterDbContext, AuctionItem auctionItem);
	}
}
