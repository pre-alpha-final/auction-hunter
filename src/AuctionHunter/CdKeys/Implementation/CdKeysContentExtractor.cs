using AuctionHunter.Infrastructure;
using Newtonsoft.Json.Linq;

namespace AuctionHunter.CdKeys.Implementation
{
	public class CdKeysContentExtractor : IContentExtractor
	{
		public JToken Extract(string item)
		{
			var token = JObject.Parse(item);
			var image = token.SelectToken("$.banner.medium")?.ToString();
			if (string.IsNullOrWhiteSpace(image))
			{
				image = token.SelectToken("$.image.medium").ToString();
			}
			return new JObject(
				new JProperty("name", token.SelectToken("$.name")),
				new JProperty("price", $"{token.SelectToken("$.minPrice")} {token.SelectToken("$.currency")}"),
				new JProperty("image", image));
		}
	}
}
