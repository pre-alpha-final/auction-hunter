using System.Collections.Generic;

namespace AuctionHunter.Infrastructure.Implementation
{
	public class AuctionHunterCore : IAuctionHunterCore
	{
		public string Name { get; set; }
		public int NumberOfPages { get; set; }
		public string BaseUrl { get; set; }
		public IUrlProvider UrlProvider { get; set; }
		public IItemsExtractor ItemsExtractor { get; set; }
		public IAuctionLinkExtractor AuctionLinkExtractor { get; set; }
		public ITitleExtractor TitleExtractor { get; set; }
		public List<string> SkipPatterns { get; set; }

		public void Run()
		{
			throw new System.NotImplementedException();
		}
	}
}
