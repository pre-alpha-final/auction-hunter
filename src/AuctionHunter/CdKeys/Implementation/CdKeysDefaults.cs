using AuctionHunter.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace AuctionHunter.CdKeys.Implementation
{
	public static class CdKeysDefaults
	{
		public static string DefaultBaseUrl => "https://www.cdkeys.com/pc/games/l/steam/price:0-99.94/";
		public static ICdKeysUrlProvider DefaultUrlProvider => Program.Container.GetService<ICdKeysUrlProvider>();
		public static IWebClient DefaultWebCllient => Program.Container.GetService<IWebClient>();
		public static ICdKeysItemsExtractor DefaultItemsExtractor => Program.Container.GetService<ICdKeysItemsExtractor>();
		public static ICdKeysAuctionLinkExtractor DefaultAuctionLinkExtractor => Program.Container.GetService<ICdKeysAuctionLinkExtractor>();
		public static ICdKeysContentExtractor DefaultContentExtractor => Program.Container.GetService<ICdKeysContentExtractor>();
	}
}
