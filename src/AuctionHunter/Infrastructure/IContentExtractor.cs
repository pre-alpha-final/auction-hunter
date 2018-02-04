using Newtonsoft.Json.Linq;

namespace AuctionHunter.Infrastructure
{
	public interface IContentExtractor
	{
		JToken Extract(string item);
	}
}
