using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AuctionHunterFront.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AuctionHunterFront.Pages
{
	[Authorize]
	public class IndexModel : PageModel
	{
		private readonly AuctionHunterDbContext _auctionHunterDbContext;

		public IList<AuctionHunterItem> AuctionHunterItems { get; set; }
		public bool HasAuctionHunterItems => AuctionHunterItems?.Count > 0;
		public string ItemIds => string.Join(",", AuctionHunterItems.Select(e => e.Id));

		public int ItemsPerPage { get; set; } = 12;

		public int ItemCount { get; set; }

		[BindProperty(SupportsGet = true)]
		public int? PageNumber { get; set; }

		[BindProperty(SupportsGet = true)]
		public bool ShowAll { get; set; }

		public IndexModel(AuctionHunterDbContext auctionHunterDbContext)
		{
			_auctionHunterDbContext = auctionHunterDbContext;
		}

		public async Task OnGetAsync()
		{
			PageNumber = PageNumber ?? 1;
			ItemCount = await _auctionHunterDbContext.AuctionHunterItems
				.Where(e => ShowAll || e.MarkedAsRead == false)
				.CountAsync();
			AuctionHunterItems = await _auctionHunterDbContext.AuctionHunterItems
				.Where(e => ShowAll || e.MarkedAsRead == false)
				.Skip(ItemsPerPage * ((int)PageNumber - 1))
				.Take(ItemsPerPage)
				.ToListAsync();
		}

		public async Task<IActionResult> OnPostMarkAsReadAsync(int id, bool showAll)
		{
			await MarkAsReadAsync(id);

			return RedirectToPage(new { showAll });
		}

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
			var update = new AuctionHunterItem { Id = id };
			_auctionHunterDbContext.AuctionHunterItems.Attach(update);

			update.MarkedAsRead = true;
			await _auctionHunterDbContext.SaveChangesAsync();
		}
	}
}
