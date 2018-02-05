using AuctionHunter.Infrastructure.Implementation;
using System.Collections.Generic;

namespace AuctionHunter.Infrastructure.Builders
{
	public class AuctionHunterCoreBuilder
	{
		private string _name;
		private int _numberOfPages;
		private int _numberOfDays;
		private string _baseUrl;
		private IUrlProvider _urlProvider;
		private IWebClient _webClient;
		private IItemsExtractor _itemsExtractor;
		private IAuctionLinkExtractor _auctionLinkExtractor;
		private IContentExtractor _contentExtractor;
		private readonly IList<string> _skipPatterns = new List<string>();

		public AuctionHunterCoreBuilder SetName(string name)
		{
			_name = name;
			return this;
		}

		public AuctionHunterCoreBuilder SetNumberOfPages(int numberOfPages)
		{
			_numberOfPages = numberOfPages;
			return this;
		}

		public AuctionHunterCoreBuilder SetNumberOfDays(int numberOfDays)
		{
			_numberOfDays = numberOfDays;
			return this;
		}

		public AuctionHunterCoreBuilder SetBaseUrl(string baseUrl)
		{
			_baseUrl = baseUrl;
			return this;
		}

		public AuctionHunterCoreBuilder SetUrlProvider(IUrlProvider urlProvider)
		{
			_urlProvider = urlProvider;
			return this;
		}

		public AuctionHunterCoreBuilder SetWebClient(IWebClient webClient)
		{
			_webClient = webClient;
			return this;
		}

		public AuctionHunterCoreBuilder SetItemsExtractor(IItemsExtractor itemsExtractor)
		{
			_itemsExtractor = itemsExtractor;
			return this;
		}

		public AuctionHunterCoreBuilder SetAuctionLinkExtractor(IAuctionLinkExtractor auctionLinkExtractor)
		{
			_auctionLinkExtractor = auctionLinkExtractor;
			return this;
		}

		public AuctionHunterCoreBuilder SetContentExtractor(IContentExtractor contentExtractor)
		{
			_contentExtractor = contentExtractor;
			return this;
		}

		public AuctionHunterCoreBuilder AddSkipPattern(string skipPattern)
		{
			_skipPatterns.Add(skipPattern);
			return this;
		}

		public IAuctionHunterCore Build()
		{
			_urlProvider.BaseUrl = _baseUrl;

			return new AuctionHunterCore
			{
				Name = _name,
				NumberOfPages = _numberOfPages,
				NumberOfDays = _numberOfDays,
				UrlProvider = _urlProvider,
				WebClient = _webClient,
				ItemsExtractor = _itemsExtractor,
				AuctionLinkExtractor = _auctionLinkExtractor,
				ContentExtractor = _contentExtractor,
				SkipPatterns = _skipPatterns,
			};
		}
	}
}