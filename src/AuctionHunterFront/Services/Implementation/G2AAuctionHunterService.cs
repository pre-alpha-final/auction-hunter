using System.Threading.Tasks;
using AuctionHunter.G2A.Implementation;
using AuctionHunter.Infrastructure;
using AuctionHunter.Infrastructure.Builders;
using AuctionHunter.Results;

namespace AuctionHunterFront.Services.Implementation
{
	public class G2AAuctionHunterService : IAuctionHunterService
	{
		private IAuctionHunterCore auctionHunterCore;

		public G2AAuctionHunterService()
		{
			auctionHunterCore = new AuctionHunterCoreBuilder()
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

		public async Task<PageResult> GetItems(int pageNumber)
		{
			return await auctionHunterCore.GetPage(pageNumber);
		}
	}
}
