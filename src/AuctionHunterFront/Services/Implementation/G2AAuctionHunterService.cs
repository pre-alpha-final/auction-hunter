using System.Threading.Tasks;
using AuctionHunter.G2A.Implementation;
using AuctionHunter.Infrastructure;
using AuctionHunter.Infrastructure.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AuctionHunterFront.Services.Implementation
{
	public class G2AAuctionHunterService : AuctionHunterServiceBase, IG2AAuctionHunterService
	{
		protected override ILogger Logger { get; set; }
		protected override IAuctionHunterCore AuctionHunterCore { get; set; }
		protected override int MaxPages { get; set; } = 100;
		protected override int DueTime { get; set; } = 1;
		protected override int Period { get; set; } = 10;

		public G2AAuctionHunterService(IConfiguration configuration, ILogger<G2AAuctionHunterService> logger)
			: base(configuration)
		{
			Logger = logger;

			AuctionHunterCore = new AuctionHunterCoreBuilder()
				.SetBaseUrl(G2ADefaults.DefaultBaseUrl)
				.SetUrlProvider(G2ADefaults.DefaultUrlProvider)
				.SetWebClient(G2ADefaults.DefaultWebCllient)
				.SetItemsExtractor(G2ADefaults.DefaultItemsExtractor)
				.SetAuctionLinkExtractor(G2ADefaults.DefaultAuctionLinkExtractor)
				.SetContentExtractor(G2ADefaults.DefaultContentExtractor)
				.AddSkipPattern("Random PREMIUM Steam Key")
				.AddSkipPattern("Random Steam Key")
				.AddSkipPattern("Steam Gift Card")
				.Build();
		}

		protected override Task AdditionalTask { get; set; } = Task.Run(async () =>
		{
			// Azure sleep hack
			await G2ADefaults.DefaultWebCllient.Get("https://auctionhunter.azurewebsites.net/Auth/Login?ReturnUrl=%2F");
		});
	}
}
