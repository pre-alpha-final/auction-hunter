using AuctionHunter.Infrastructure;
using AuctionHunterFront.Models;
using AuctionHunterFront.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AuctionHunterFront.Pages.Update
{
	[Authorize]
	public class RunModel : PageModel
	{
		private readonly AuctionHunterDbContext _auctionHunterDbContext;
		private readonly IAuctionHunterService _auctionHunterService;

		[BindProperty(SupportsGet = true)]
		public int? PageNumber { get; set; }

		[BindProperty(SupportsGet = true)]
		public int? MaxPage { get; set; }

		public string DebugInfo { get; set; }

		public RunModel(AuctionHunterDbContext auctionHunterDbContext, IAuctionHunterService auctionHunterService)
		{
			_auctionHunterDbContext = auctionHunterDbContext;
			_auctionHunterService = auctionHunterService;
		}

		public async Task<IActionResult> OnGetContinuousPullAsync()
		{
			var pageResult = await _auctionHunterService.GetItems(PageNumber ?? -1);
			foreach (var auctionItem in pageResult.AuctionItems.ToList())
			{
				await TryAddAsync(auctionItem);
			}
			await _auctionHunterDbContext.SaveChangesAsync();
			DebugInfo = pageResult.DebugInfo.Replace("\n", "<br />");

			return Page();
		}

		private async Task TryAddAsync(AuctionItem auctionItem)
		{
			try
			{
				var oldItem = _auctionHunterDbContext.AuctionHunterItems
					.FirstOrDefault(e => e.AuctionLink == auctionItem.AuctionLink);
				if (oldItem != null)
					return;

				var item = await _auctionHunterDbContext.AuctionHunterItems.AddAsync(new AuctionHunterItem
				{
					AuctionLink = auctionItem.AuctionLink,
					OnPage = auctionItem.OnPage,
					Timestamp = auctionItem.Timestamp,
					ContentJson = auctionItem.ContentJson,
				});

				var users = await _auctionHunterDbContext.Users.ToListAsync();
				foreach (var user in users)
				{
					await _auctionHunterDbContext.ApplicationUserAuctionHunterItems.AddAsync(new ApplicationUserAuctionHunterItem
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