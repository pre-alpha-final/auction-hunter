using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using AuctionHunter.Infrastructure;
using AuctionHunter.Results;
using AuctionHunterFront.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AuctionHunterFront.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace AuctionHunterFront.Services.Implementation
{
	public abstract class AuctionHunterServiceBase
	{
		private readonly IConfiguration _configuration;
		private readonly IServiceProvider _serviceProvider;
		private Timer _aTimer;
		private int _currentPageNumber = 1;

		protected Func<Task> AdditionalTask { get; set; }

		protected abstract ILogger Logger { get; set; }
		protected abstract IAuctionHunterCore AuctionHunterCore { get; set; }
		protected abstract int MaxPages { get; set; }
		protected abstract int DueTime { get; set; }
		protected abstract int Period { get; set; }

		public AuctionHunterServiceBase(IConfiguration configuration, IServiceProvider serviceProvider)
		{
			_configuration = configuration;
			_serviceProvider = serviceProvider;
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

		public async Task TryAddAsync(AuctionHunterDbContext auctionHunterDbContext, AuctionItem auctionItem)
		{
			try
			{
				var oldItem = auctionHunterDbContext.AuctionHunterItems
					.FirstOrDefault(e => e.AuctionLink == auctionItem.AuctionLink);
				if (oldItem != null)
				{
					if (PriceDropped(oldItem, auctionItem, 0.33m))
					{
						auctionHunterDbContext.AuctionHunterItems.Remove(oldItem);
					}
					else
					{
						return;
					}
				}

				var item = await auctionHunterDbContext.AuctionHunterItems.AddAsync(new AuctionHunterItem
				{
					AuctionLink = auctionItem.AuctionLink,
					OnPage = auctionItem.OnPage,
					Timestamp = auctionItem.Timestamp,
					ContentJson = auctionItem.ContentJson,
				});

				var userDbContext = _serviceProvider.GetService<UsersDbContext>();
				var users = await userDbContext.Users.ToListAsync();
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
					await AdditionalTask();
				}
			}
			catch (Exception e)
			{
				Logger.LogError(e.ToString());
			}
		}

		private bool PriceDropped(AuctionHunterItem oldItem, AuctionItem newItem, decimal ratio)
		{
			try
			{
				var newPrice = GetPrice(newItem.ContentJson);
				var oldPrice = GetPrice(oldItem.ContentJson);

				return newPrice < oldPrice * (1 - ratio);
			}
			catch (Exception e)
			{
				Logger.LogError($"PriceDropped error: {e.Message}");
			}

			return false;
		}

		private decimal GetPrice(string contentJson)
		{
			var priceToken = JToken.Parse(contentJson).SelectToken("$.rawPrice");
			var price = ((JValue)priceToken).ToString(new CultureInfo("en-US"));

			return decimal.Parse(price, new CultureInfo("en-US"));
		}
	}
}
