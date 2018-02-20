using AuctionHunter.Results;
using System.Threading.Tasks;

namespace AuctionHunterFront.Services
{
	public interface IAuctionHunterService
    {
		Task<PageResult> GetItems(int pageNumber); 
    }
}
