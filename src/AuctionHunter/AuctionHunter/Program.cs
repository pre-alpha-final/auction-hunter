using AuctionHunter.G2A.Implementation;
using AuctionHunter.Infrastructure;
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

			var auctionHunter = Container.GetService<IAuctionHunterCore>();
			auctionHunter.Run();

			Console.WriteLine("\nDone");
			Console.ReadKey(true);
		}

		private static void RegisterServices()
		{
			var services = new ServiceCollection();
			services.AddTransient<IAuctionHunterCore, AuctionHunterCore>();
			services.AddTransient<IWebClient, DefaultWebClient>();
			services.AddTransient<IAuctionLinkExtractor, G2AAuctionLinkExtractor>();
			services.AddTransient<IItemsExtractor, G2AItemsExtractor>();
			services.AddTransient<ITitleExtractor, G2ATitleExtractor>();
			services.AddTransient<IUrlProvider, G2AUrlProvider>();
			Container = services.BuildServiceProvider();
		}
	}
}
