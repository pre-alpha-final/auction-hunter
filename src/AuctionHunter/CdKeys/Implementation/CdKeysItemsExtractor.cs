using System.Collections.Generic;
using System.Linq;
using AuctionHunter.Infrastructure;
using Newtonsoft.Json.Linq;

namespace AuctionHunter.CdKeys.Implementation
{
	public class CdKeysItemsExtractor : IItemsExtractor
	{
		public IList<string> GetItems(string page)
		{
			var token = JObject.Parse(page);
			var items = token.SelectTokens("$.products[*]").Select(e => e.ToString()).ToList();

			return items;
		}
	}
}
