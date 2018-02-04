using System.Threading.Tasks;

namespace AuctionHunter.Infrastructure
{
	public interface IWebClient
	{
		Task<string> Get(string url);
	}
}
