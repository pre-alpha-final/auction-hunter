using AuctionHunter.Infrastructure;
using Newtonsoft.Json.Linq;

namespace AuctionHunter.G2A.Implementation
{
	public class G2ATitleExtractor : ITitleExtractor
	{
		public string Extract(string item)
		{
			var token = JObject.Parse(item);
			return token.SelectToken("$.name").ToString();
		}
	}
}
