using AuctionHunter.CdKeys.Implementation;
using AuctionHunter.Infrastructure;
using AuctionHunter.Infrastructure.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AuctionHunterFront.Services.Implementation
{
	public class CdKeysAuctionHunterService : AuctionHunterServiceBase, ICdKeysAuctionHunterService
	{
		protected override ILogger Logger { get; set; }
		protected override IAuctionHunterCore AuctionHunterCore { get; set; }
		protected override int MaxPages { get; set; } = 1;
		protected override int DueTime { get; set; } = 0;
		protected override int Period { get; set; } = 10;

		public CdKeysAuctionHunterService(IConfiguration configuration, ILogger<CdKeysAuctionHunterService> logger)
			: base(configuration)
		{
			Logger = logger;

			AuctionHunterCore = new AuctionHunterCoreBuilder()
				.SetBaseUrl(CdKeysDefaults.DefaultBaseUrl)
				.SetUrlProvider(CdKeysDefaults.DefaultUrlProvider)
				.SetWebClient(CdKeysDefaults.DefaultWebCllient)
				.SetItemsExtractor(CdKeysDefaults.DefaultItemsExtractor)
				.SetAuctionLinkExtractor(CdKeysDefaults.DefaultAuctionLinkExtractor)
				.SetContentExtractor(CdKeysDefaults.DefaultContentExtractor)
				.AddSkipPattern("Random PREMIUM Steam Key")
				.AddSkipPattern("Random Steam Key")
				.AddSkipPattern("Steam Gift Card")
				.Build();
		}
	}
}
