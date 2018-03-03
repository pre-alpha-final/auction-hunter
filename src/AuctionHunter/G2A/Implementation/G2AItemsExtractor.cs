using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace AuctionHunter.G2A.Implementation
{
	public class G2AItemsExtractor : IG2AItemsExtractor
	{
		public IList<string> GetItems(string page)
		{
			var token = JObject.Parse(page);
			var items = token.SelectTokens("$.products[*]").Select(e => e.ToString()).ToList();

			return items;
		}
	}
}
