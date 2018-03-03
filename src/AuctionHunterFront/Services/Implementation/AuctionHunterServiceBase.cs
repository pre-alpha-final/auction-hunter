using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using AuctionHunter.Infrastructure;
using AuctionHunter.Results;
using AuctionHunterFront.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AuctionHunterFront.Extensions;
using Microsoft.Extensions.Logging;

namespace AuctionHunterFront.Services.Implementation
{
	public abstract class AuctionHunterServiceBase
	{
		private readonly IConfiguration _configuration;
		private Timer _aTimer;
		private int _currentPageNumber = 1;

		protected abstract ILogger Logger { get; set; }
		protected abstract IAuctionHunterCore AuctionHunterCore { get; set; }
		protected abstract int MaxPages { get; set; }
		protected abstract int DueTime { get; set; }
		protected abstract int Period { get; set; }
		protected virtual Task AdditionalTask { get; set; }

		public AuctionHunterServiceBase(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public Task Start()
		{
			if (_aTimer == null)
				_aTimer = new Timer(OnTimerOnElapsed, null, TimeSpan.FromMinutes(DueTime), TimeSpan.FromMinutes(Period));

			return Task.CompletedTask;
		}

		public async Task<PageResult> GetItems(int pageNumber)
		{
			return await AuctionHunterCore.GetPage(pageNumber);
		}

		private async void OnTimerOnElapsed(object state)
		{
			try
			{
				var pageResult = await GetItems(_currentPageNumber);
				Logger.LogInformation($"Item count: {pageResult.AuctionItems.Count}");
				Logger.LogInformation(pageResult.DebugInfo);

				using (var dbContext = new AuctionHunterDbContext(_configuration))
				{
					foreach (var auctionItem in pageResult.AuctionItems.ToList())
					{
						await TryAddAsync(dbContext, auctionItem);
					}
					await dbContext.SafeSaveChangesAsync();
				}
				_currentPageNumber = _currentPageNumber % MaxPages + 1;

				if (AdditionalTask != null)
				{
					await AdditionalTask;
				}
			}
			catch (Exception e)
			{
				Logger.LogError(e.ToString());
			}
		}

		private async Task TryAddAsync(AuctionHunterDbContext auctionHunterDbContext, AuctionItem auctionItem)
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
				Logger.LogError(e.ToString());
			}
		}
	}
}
