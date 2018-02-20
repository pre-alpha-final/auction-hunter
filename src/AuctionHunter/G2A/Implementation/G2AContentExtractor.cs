using AuctionHunter.Infrastructure;
using Newtonsoft.Json.Linq;

namespace AuctionHunter.G2A.Implementation
{
	public class G2AContentExtractor : IContentExtractor
	{
		public JToken Extract(string item)
		{
			var token = JObject.Parse(item);
			return new JObject(
				new JProperty("name", token.SelectToken("$.name")),
				new JProperty("price", $"{token.SelectToken("$.minPrice")} {token.SelectToken("$.currency")}"),
				new JProperty("image", token.SelectToken("$.banner.medium")));
		}
	}
}
