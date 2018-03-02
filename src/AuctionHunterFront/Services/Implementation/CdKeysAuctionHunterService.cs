using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using AuctionHunter.CdKeys.Implementation;
using AuctionHunter.Infrastructure;
using AuctionHunter.Infrastructure.Builders;
using AuctionHunter.Results;
using AuctionHunterFront.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AuctionHunterFront.Extensions;
using Microsoft.Extensions.Logging;

namespace AuctionHunterFront.Services.Implementation
{
	public class CdKeysAuctionHunterService : IAuctionHunterService
	{
		private readonly IConfiguration _configuration;
		private readonly ILogger<CdKeysAuctionHunterService> _logger;
		private readonly IAuctionHunterCore _auctionHunterCore;
		private Timer _aTimer;
		private int _currentPageNumber = 1;

		public CdKeysAuctionHunterService(IConfiguration configuration, ILogger<CdKeysAuctionHunterService> logger)
		{
			_configuration = configuration;
			_logger = logger;

			_auctionHunterCore = new AuctionHunterCoreBuilder()
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

		public Task Start()
		{
			if (_aTimer == null)
				_aTimer = new Timer(OnTimerOnElapsed, null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(10));

			return Task.CompletedTask;
		}

		public async Task<PageResult> GetItems(int pageNumber)
		{
			return await _auctionHunterCore.GetPage(pageNumber);
		}

		private async void OnTimerOnElapsed(object state)
		{
			var pageResult = await GetItems(_currentPageNumber);
			_logger.LogInformation($"Item count: {pageResult.AuctionItems.Count}\n");
			_logger.LogInformation(pageResult.DebugInfo);

			try
			{
				using (var dbContext = new AuctionHunterDbContext(_configuration))
				{
					foreach (var auctionItem in pageResult.AuctionItems.ToList())
					{
						await TryAddAsync(dbContext, auctionItem);
					}
					await dbContext.SafeSaveChangesAsync();
				}
				_currentPageNumber = _currentPageNumber % 100 + 1;
			}
			catch (Exception e)
			{
				_logger.LogError(e.ToString());
			}
		}

		private static async Task TryAddAsync(AuctionHunterDbContext auctionHunterDbContext, AuctionItem auctionItem)
		{
			try
			{
				var oldItem = auctionHunterDbContext.AuctionHunterItems
					.FirstOrDefault(e => e.AuctionLink == auctionItem.AuctionLink);
				if (oldItem != null)
					return;

				var item = await auctionHunterDbContext.AuctionHunterItems.AddAsync(new AuctionHunterItem
				{
					AuctionLink = auctionItem.AuctionLink,
					OnPage = auctionItem.OnPage,
					Timestamp = auctionItem.Timestamp,
					ContentJson = auctionItem.ContentJson,
				});

				var users = await auctionHunterDbContext.Users.ToListAsync();
				foreach (var user in users)
				{
					await auctionHunterDbContext.ApplicationUserAuctionHunterItems.AddAsync(new ApplicationUserAuctionHunterItem
					{
						ApplicationUser = user,
						AuctionHunterItem = item.Entity,
					});
				}
			}
			catch (Exception e)
			{
				// ignore
			}
		}
	}
}
