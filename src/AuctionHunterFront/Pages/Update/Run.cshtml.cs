﻿using AuctionHunter.Infrastructure;
using AuctionHunterFront.Models;
using AuctionHunterFront.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using System.Threading.Tasks;

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
			var pageResult = await _auctionHunterService.GetItems((int)PageNumber);
			pageResult.AuctionItems.ToList().ForEach(async e => await TryAddAsync(e));
			await _auctionHunterDbContext.SaveChangesAsync();
			DebugInfo = pageResult.DebugInfo;

			return Page();
		}

		private async Task TryAddAsync(AuctionItem auctionItem)
		{
			try
			{
				await _auctionHunterDbContext.AuctionHunterItems.AddAsync(new AuctionHunterItem
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
				// Ignore
			}
		}
	}
}