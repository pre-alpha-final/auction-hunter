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
		private ITitleExtractor _titleExtractor;
		private IAuctionLinkExtractor _auctionLinkExtractor;
		private List<string> _skipPatterns = new List<string>();

		AuctionHunterCoreBuilder SetName(string name)
		{
			_name = name;
			return this;
		}

		AuctionHunterCoreBuilder SetNumberOfPages(int numberOfPages)
		{
			_numberOfPages = numberOfPages;
			return this;
		}

		AuctionHunterCoreBuilder SetNumberOfDays(int numberOfDays)
		{
			_numberOfDays = numberOfDays;
			return this;
		}

		AuctionHunterCoreBuilder SetBaseUrl(string baseUrl)
		{
			_baseUrl = baseUrl;
			return this;
		}

		AuctionHunterCoreBuilder SetUrlProvider(IUrlProvider urlProvider)
		{
			_urlProvider = urlProvider;
			return this;
		}

		AuctionHunterCoreBuilder SetUrlProvider(IWebClient webClient)
		{
			_webClient = webClient;
			return this;
		}

		AuctionHunterCoreBuilder SetItemsExtractor(IItemsExtractor itemsExtractor)
		{
			_itemsExtractor = itemsExtractor;
			return this;
		}

		AuctionHunterCoreBuilder SetTitleExtractor(ITitleExtractor titleExtractor)
		{
			_titleExtractor = titleExtractor;
			return this;
		}

		AuctionHunterCoreBuilder SetAuctionLinkExtractor(IAuctionLinkExtractor auctionLinkExtractor)
		{
			_auctionLinkExtractor = auctionLinkExtractor;
			return this;
		}

		AuctionHunterCoreBuilder AddSkipPattern(string skipPattern)
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
				TitleExtractor = _titleExtractor,
				AuctionLinkExtractor = _auctionLinkExtractor,
				SkipPatterns = _skipPatterns,
			};
		}
	}
}