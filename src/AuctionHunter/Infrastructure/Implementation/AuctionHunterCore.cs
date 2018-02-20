using AuctionHunter.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionHunter.Infrastructure.Implementation
{
	public class AuctionHunterCore : IAuctionHunterCore
	{
		public IUrlProvider UrlProvider { get; set; }
		public IWebClient WebClient { get; set; }
		public IItemsExtractor ItemsExtractor { get; set; }
		public IAuctionLinkExtractor AuctionLinkExtractor { get; set; }
		public IContentExtractor ContentExtractor { get; set; }
		public IList<string> SkipPatterns { get; set; }

		public async Task<PageResult> GetPage(int pageNumber)
		{
			var pageResult = new PageResult
			{
				DebugInfo = $"Doing page number: {pageNumber}\n"
			};

			var url = UrlProvider.GetUrlForPage(pageNumber);
			var webClientResult = await WebClient.Get(url);
			pageResult.DebugInfo += webClientResult.DebugInfo;
			if (webClientResult.Success == false)
			{
				return pageResult;
			}

			var items = ItemsExtractor.GetItems(webClientResult.Content);
			pageResult.AuctionItems = GetAuctionItems(items);
			foreach (var auctionItem in pageResult.AuctionItems)
			{
				auctionItem.OnPage = pageNumber;
			}
			pageResult.Success = true;

			return pageResult;
		}

		private IList<AuctionItem> GetAuctionItems(IEnumerable<string> items)
		{
			return items.Select(item => new AuctionItem
				{
					AuctionLink = AuctionLinkExtractor.Extract(item),
					ContentJson = ContentExtractor.Extract(item).ToString(),
					Timestamp = DateTime.UtcNow,
				})
				.Where(auctionItem => !SkipPatterns.Any(e => auctionItem.ContentJson.Contains(e)))
				.ToList();
		}
	}
}
