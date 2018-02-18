using AuctionHunter.Results;
using System.Threading.Tasks;

namespace AuctionHunter.Infrastructure
{
	public interface IWebClient
	{
		Task<WebClientResult> Get(string url);
	}
}
