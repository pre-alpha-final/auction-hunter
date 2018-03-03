using System.Collections.Generic;
using AuctionHunter.Extensions;
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
			var htmlNodeCollection = htmlDocument.DocumentNode.SafeSelectNodes("//li[@class='item']");
			if (htmlNodeCollection == null)
				return new List<string>();

			foreach (HtmlNode htmlNode in htmlNodeCollection)
			{
				items.Add(htmlNode.InnerHtml);
			}

			return items;
		}
	}
}
