using AuctionHunter.Results;
using System.Threading.Tasks;

namespace AuctionHunter.Infrastructure
{
	public interface IAuctionHunterCore
	{
		Task<PageResult> GetPage(int pageNumber);
	}
}
