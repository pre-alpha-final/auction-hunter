using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace AuctionHunter.CdKeys.Implementation
{
	public class CdKeysItemsExtractor : ICdKeysItemsExtractor
	{
		public IList<string> GetItems(string page)
		{
			var token = JObject.Parse(page);
			var items = token.SelectTokens("$.products[*]").Select(e => e.ToString()).ToList();

			return items;
		}
	}
}
