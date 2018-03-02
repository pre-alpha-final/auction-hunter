using AuctionHunter.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace AuctionHunter.G2A.Implementation
{
	public static class G2ADefaults
	{
		public static string DefaultBaseUrl => "https://www.g2a.com/new/api/products/filter?category_id=games&changeType=PAGINATION&currency=PLN&min_price[max]=100&min_price[min]=0&page=&platform=1&store=polish";
		public static IG2AUrlProvider DefaultUrlProvider => Program.Container.GetService<IG2AUrlProvider>();
		public static IWebClient DefaultWebCllient => Program.Container.GetService<IWebClient>();
		public static IG2AItemsExtractor DefaultItemsExtractor => Program.Container.GetService<IG2AItemsExtractor>();
		public static IG2AAuctionLinkExtractor DefaultAuctionLinkExtractor => Program.Container.GetService<IG2AAuctionLinkExtractor>();
		public static IG2AContentExtractor DefaultContentExtractor => Program.Container.GetService<IG2AContentExtractor>();
	}
}
