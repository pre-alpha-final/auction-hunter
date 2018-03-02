using System.Collections.Generic;
using HtmlAgilityPack;

namespace AuctionHunter.CdKeys.Implementation
{
	public class CdKeysItemsExtractor : ICdKeysItemsExtractor
	{
		public IList<string> GetItems(string page)
		{
			var htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(page);

			var items = new List<string>();
			var htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//li[@class='item']");
			foreach (HtmlNode htmlNode in htmlNodeCollection)
			{
				items.Add(htmlNode.InnerHtml);
			}

			return items;
		}
	}
}
