using AuctionHunter.G2A;
using AuctionHunter.G2A.Implementation;
using AuctionHunter.Infrastructure;
using AuctionHunter.Infrastructure.Builders;
using AuctionHunter.Infrastructure.Implementation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace AuctionHunter
{
	internal class Program
	{
		public static IServiceProvider Container { get; private set; }

		static async Task Main(string[] args)
		{
			Console.WriteLine("Booting up\n");

			RegisterServices();

			var auctionHunterCoreBuilder = new AuctionHunterCoreBuilder();
			var auctionHunter = auctionHunterCoreBuilder
				.SetBaseUrl("https://www.g2a.com/new/api/products/filter?category_id=games&changeType=PAGINATION&currency=PLN&min_price[max]=100&min_price[min]=0&page=&platform=1&store=polish")
				.SetUrlProvider(Container.GetService<IUrlProvider>())
				.SetWebClient(Container.GetService<IWebClient>())
				.SetItemsExtractor(Container.GetService<IItemsExtractor>())
				.SetAuctionLinkExtractor(Container.GetService<IAuctionLinkExtractor>())
				.SetContentExtractor(Container.GetService<IContentExtractor>())
				.AddSkipPattern("Random PREMIUM Steam Key")
				.AddSkipPattern("Random Steam Key")
				.AddSkipPattern("Steam Gift Card")
				.Build();

			for (int i = 1; i < 5; i++)
			{
				var page = await auctionHunter.GetPage(i);
				Console.Write(page.DebugInfo);
			}

			Console.WriteLine("\nDone");
			Console.ReadKey(true);
		}

		public static void RegisterServices()
		{
			var services = new ServiceCollection();
			services.AddTransient<IWebClient, DefaultWebClient>();

			services.AddTransient<IG2AAuctionLinkExtractor, G2AAuctionLinkExtractor>();
			services.AddTransient<IG2AItemsExtractor, G2AItemsExtractor>();
			services.AddTransient<IG2AContentExtractor, G2AContentExtractor>();
			services.AddTransient<IG2AUrlProvider, G2AUrlProvider>();

			Container = services.BuildServiceProvider();
		}
	}
}
