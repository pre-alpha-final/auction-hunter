using AuctionHunter.Results;
using System.Threading.Tasks;

namespace AuctionHunterFront.Services
{
	public interface IAuctionHunterService
	{
		Task Start();
		Task<PageResult> GetItems(int pageNumber);
	}
}
