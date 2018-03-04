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
using AuctionHunterFront.Extensions;

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
				await _auctionHunterService.TryAddAsync(_auctionHunterDbContext, auctionItem);
			}
			await _auctionHunterDbContext.SafeSaveChangesAsync();
			DebugInfo = pageResult.DebugInfo.Replace("\n", "<br />");

			return Page();
		}
	}
}