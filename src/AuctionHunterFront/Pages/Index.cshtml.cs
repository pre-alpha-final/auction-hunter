using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AuctionHunterFront.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using AuctionHunterFront.Extensions;

namespace AuctionHunterFront.Pages
{
	[Authorize]
	public class IndexModel : PageModel
	{
		private readonly AuctionHunterDbContext _auctionHunterDbContext;
		private readonly UserManager<ApplicationUser> _userManager;

		public IList<AuctionHunterItem> AuctionHunterItems { get; set; }
		public bool HasAuctionHunterItems => AuctionHunterItems?.Count > 0;
		public string ItemIds => string.Join(",", AuctionHunterItems.Select(e => e.Id));

		public int ItemsPerPage { get; set; } = 12;

		public int ItemCount { get; set; }

		[BindProperty(SupportsGet = true)]
		public int? PageNumber { get; set; }

		[BindProperty(SupportsGet = true)]
		public bool ShowAll { get; set; }

		public IndexModel(AuctionHunterDbContext auctionHunterDbContext, UserManager<ApplicationUser> userManager)
		{
			_auctionHunterDbContext = auctionHunterDbContext;
			_userManager = userManager;
		}

		public async Task OnGetAsync()
		{
			var currentUser = await _userManager.GetUserAsync(HttpContext.User);
			PageNumber = PageNumber ?? 1;

			ItemCount = await _auctionHunterDbContext.ApplicationUserAuctionHunterItems
				.Where(e => ShowAll || e.ApplicationUser == currentUser)
				.CountAsync();

			AuctionHunterItems = await _auctionHunterDbContext.ApplicationUserAuctionHunterItems
				.Where(e => ShowAll || e.ApplicationUser == currentUser)
				.Select(e => e.AuctionHunterItem)
				.Skip(ItemsPerPage * ((int)PageNumber - 1))
				.Take(ItemsPerPage)
				.ToListAsync();
		}

		[ValidateAntiForgeryToken]
		public async Task<IActionResult> OnPostMarkAsReadAsync(int id, bool showAll)
		{
			await MarkAsReadAsync(id);

			return RedirectToPage(new { showAll });
		}

		[ValidateAntiForgeryToken]
		public async Task<IActionResult> OnPostMarkAllAsReadAsync(string itemIds, bool showAll)
		{
			foreach (var id in itemIds.Split(',', System.StringSplitOptions.RemoveEmptyEntries))
			{
				await MarkAsReadAsync(int.Parse(id));
			}

			var pageNumber = 1;
			return RedirectToPage(new { pageNumber, showAll });
		}

		public JToken JsonParse(string json)
		{
			return JObject.Parse(json);
		}

		private async Task MarkAsReadAsync(int id)
		{
			var currentUser = await _userManager.GetUserAsync(HttpContext.User);
			var item = await _auctionHunterDbContext.ApplicationUserAuctionHunterItems
				.Where(e => e.ApplicationUser == currentUser && e.AuctionHunterItem.Id == id)
				.FirstOrDefaultAsync();

			if (item != null)
			{
				_auctionHunterDbContext.ApplicationUserAuctionHunterItems.Remove(item);
				await _auctionHunterDbContext.SafeSaveChangesAsync();
			}
		}
	}
}
