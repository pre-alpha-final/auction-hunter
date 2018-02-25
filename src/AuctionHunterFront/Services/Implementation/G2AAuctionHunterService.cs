using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using AuctionHunter.G2A.Implementation;
using AuctionHunter.Infrastructure;
using AuctionHunter.Infrastructure.Builders;
using AuctionHunter.Results;
using AuctionHunterFront.Models;
using Microsoft.Extensions.Configuration;

namespace AuctionHunterFront.Services.Implementation
{
	public class G2AAuctionHunterService : IAuctionHunterService
	{
		private readonly IConfiguration _configuration;
		private readonly IAuctionHunterCore _auctionHunterCore;
		private readonly Timer _aTimer;
		private int _currentPageNumber = 1;

		public G2AAuctionHunterService(IConfiguration configuration)
		{
			_configuration = configuration;

			_auctionHunterCore = new AuctionHunterCoreBuilder()
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

			_aTimer = new Timer(OnTimerOnElapsed, null, TimeSpan.FromMinutes(10), TimeSpan.FromMinutes(10));
		}

		public async Task<PageResult> GetItems(int pageNumber)
		{
			return await _auctionHunterCore.GetPage(pageNumber);
		}

		private async void OnTimerOnElapsed(object state)
		{
			// Azure sleep hack
			await G2ADefaults.DefaultWebCllient.Get("https://auctionhunter.azurewebsites.net/");

			var pageResult = await GetItems(_currentPageNumber);
			using (var dbContext = new AuctionHunterDbContext(_configuration))
			{
				foreach (var auctionItem in pageResult.AuctionItems.ToList())
				{
					await TryAddAsync(dbContext, auctionItem);
				}
				await dbContext.SaveChangesAsync();
			}
			_currentPageNumber = _currentPageNumber % 100 + 1;
		}

		private static async Task TryAddAsync(AuctionHunterDbContext auctionHunterDbContext, AuctionItem auctionItem)
		{
			try
			{
				var oldItem = auctionHunterDbContext.AuctionHunterItems
					.FirstOrDefault(e => e.AuctionLink == auctionItem.AuctionLink);
				if (oldItem != null)
					return;

				await auctionHunterDbContext.AuctionHunterItems.AddAsync(new AuctionHunterItem
				{
					AuctionLink = auctionItem.AuctionLink,
					OnPage = auctionItem.OnPage,
					MarkedAsRead = false,
					Timestamp = auctionItem.Timestamp,
					ContentJson = auctionItem.ContentJson,
				});
			}
			catch (Exception e)
			{
				// ignore
			}
		}
	}
}
