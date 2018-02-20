using AuctionHunter.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace AuctionHunter.G2A.Implementation
{
	public static class G2ADefaults
    {
		// TODO: do this better
		public static void Initialize()
		{
			if (Program.Container == null)
				Program.RegisterServices();
		}

		public static string DefaultBaseUrl => "https://www.g2a.com/new/api/products/filter?category_id=games&changeType=PAGINATION&currency=PLN&min_price[max]=100&min_price[min]=0&page=&platform=1&store=polish";
		public static IUrlProvider DefaultUrlProvider => Program.Container.GetService<IUrlProvider>();
		public static IWebClient DefaultWebCllient => Program.Container.GetService<IWebClient>();
		public static IItemsExtractor DefaultItemsExtractor => Program.Container.GetService<IItemsExtractor>();
		public static IAuctionLinkExtractor DefaultAuctionLinkExtractor => Program.Container.GetService<IAuctionLinkExtractor>();
		public static IContentExtractor DefaultContentExtractor => Program.Container.GetService<IContentExtractor>();
    }
}
