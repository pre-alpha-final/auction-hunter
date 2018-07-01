using AuctionHunter.Results;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace AuctionHunter.Infrastructure
{
	public interface IWebClient
	{
		Task<WebClientResult> Get(string url, int retryCount = 5);
		Task<WebClientResult> Post(string url, NameValueCollection values, int retryCount = 5);
	}
}
