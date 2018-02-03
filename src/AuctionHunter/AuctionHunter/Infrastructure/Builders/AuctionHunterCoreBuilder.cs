using AuctionHunter.Infrastructure.Implementation;
using System.Collections.Generic;

namespace AuctionHunter.Infrastructure.Builders
{
	public class AuctionHunterCoreBuilder
	{
		private string _name;
		private int _numberOfPages;
		private string _baseUrl;
		private IUrlProvider _urlProvider;
		private IItemsExtractor _itemsExtractor;
		private IAuctionLinkExtractor _auctionLinkExtractor;
		private ITitleExtractor _titleExtractor;
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

		AuctionHunterCoreBuilder SetUrlProvider(string baseUrl, IUrlProvider urlProvider)
		{
			_baseUrl = baseUrl;
			_urlProvider = urlProvider;
			return this;
		}

		AuctionHunterCoreBuilder SetItemsExtractor(IItemsExtractor itemsExtractor)
		{
			_itemsExtractor = itemsExtractor;
			return this;
		}

		AuctionHunterCoreBuilder SetAuctionLinkExtractor(IAuctionLinkExtractor auctionLinkExtractor)
		{
			_auctionLinkExtractor = auctionLinkExtractor;
			return this;
		}

		AuctionHunterCoreBuilder SetTitleExtractor(ITitleExtractor titleExtractor)
		{
			_titleExtractor = titleExtractor;
			return this;
		}

		AuctionHunterCoreBuilder AddSkipPattern(string skipPattern)
		{
			_skipPatterns.Add(skipPattern);
			return this;
		}

		public IAuctionHunterCore Build()
		{
			return new AuctionHunterCore
			{
				Name = _name,
				NumberOfPages = _numberOfPages,
				BaseUrl = _baseUrl,
				UrlProvider = _urlProvider,
				ItemsExtractor = _itemsExtractor,
				AuctionLinkExtractor = _auctionLinkExtractor,
				TitleExtractor = _titleExtractor,
				SkipPatterns = _skipPatterns,
			};
		}
	}
}