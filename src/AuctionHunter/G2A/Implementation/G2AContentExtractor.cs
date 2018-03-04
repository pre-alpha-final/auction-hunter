using Newtonsoft.Json.Linq;
using System.Globalization;

namespace AuctionHunter.G2A.Implementation
{
	public class G2AContentExtractor : IG2AContentExtractor
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
				new JProperty("rawPrice", ((JValue)token.SelectToken("$.minPrice")).ToString(new CultureInfo("en-US"))),
				new JProperty("price", $"{token.SelectToken("$.minPrice")} {token.SelectToken("$.currency")}"),
				new JProperty("image", image));
		}
	}
}
