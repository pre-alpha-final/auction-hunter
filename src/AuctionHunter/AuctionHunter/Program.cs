using AuctionHunter.G2A.Implementation;
using AuctionHunter.Infrastructure;
using AuctionHunter.Infrastructure.Builders;
using AuctionHunter.Infrastructure.Implementation;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AuctionHunter
{
	class Program
    {
		public static IServiceProvider Container { get; private set; }

		static void Main(string[] args)
        {
            Console.WriteLine("Booting up\n");

			RegisterServices();

			var auctionHunterCoreBuilder = new AuctionHunterCoreBuilder();
			var auctionHunter = auctionHunterCoreBuilder
				.SetName("G2A")
				.SetNumberOfPages(2)
				.SetNumberOfDays(7)
				.SetBaseUrl("https://www.g2a.com/new/api/products/filter?category_id=games&changeType=PAGINATION&currency=PLN&min_price[max]=100&min_price[min]=0&page=&platform=1&store=polish")
				.SetUrlProvider(Container.GetService<IUrlProvider>())
				.SetWebClient(Container.GetService<IWebClient>())
				.SetItemsExtractor(Container.GetService<IItemsExtractor>())
				.SetTitleExtractor(Container.GetService<ITitleExtractor>())
				.SetAuctionLinkExtractor(Container.GetService<IAuctionLinkExtractor>())
				.Build();
			auctionHunter.Run();

			Console.WriteLine("\nDone");
			Console.ReadKey(true);
		}

		private static void RegisterServices()
		{
			var services = new ServiceCollection();
			services.AddTransient<IWebClient, DefaultWebClient>();
			services.AddTransient<IAuctionLinkExtractor, G2AAuctionLinkExtractor>();
			services.AddTransient<IItemsExtractor, G2AItemsExtractor>();
			services.AddTransient<ITitleExtractor, G2ATitleExtractor>();
			services.AddTransient<IUrlProvider, G2AUrlProvider>();
			Container = services.BuildServiceProvider();
		}
	}
}
