using AuctionHunter.CdKeys.Implementation;
using AuctionHunter.Infrastructure;
using AuctionHunter.Infrastructure.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace AuctionHunterFront.Services.Implementation
{
	public class CdKeysAuctionHunterService : AuctionHunterServiceBase, ICdKeysAuctionHunterService
	{
		protected override ILogger Logger { get; set; }
		protected override IAuctionHunterCore AuctionHunterCore { get; set; }
		protected override int MaxPages { get; set; } = 20;
		protected override int DueTime { get; set; } = 2;
		protected override int Period { get; set; } = 20;

		public CdKeysAuctionHunterService(IConfiguration configuration, ILogger<CdKeysAuctionHunterService> logger, IServiceProvider serviceProvider)
			: base(configuration, serviceProvider)
		{
			Logger = logger;

			AuctionHunterCore = new AuctionHunterCoreBuilder()
				.SetBaseUrl(CdKeysDefaults.DefaultBaseUrl)
				.SetUrlProvider(CdKeysDefaults.DefaultUrlProvider)
				.SetWebClient(CdKeysDefaults.DefaultWebCllient)
				.SetItemsExtractor(CdKeysDefaults.DefaultItemsExtractor)
				.SetAuctionLinkExtractor(CdKeysDefaults.DefaultAuctionLinkExtractor)
				.SetContentExtractor(CdKeysDefaults.DefaultContentExtractor)
				.AddSkipPattern("Out of stock")
				.Build();
		}
	}
}
