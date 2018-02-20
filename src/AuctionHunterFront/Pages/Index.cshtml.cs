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

		public int ItemsPerPage { get; set; } = 12;

		public int ItemCount { get; set; }

		[BindProperty(SupportsGet = true)]
		public int? PageNumber { get; set; }

		public IndexModel(AuctionHunterDbContext auctionHunterDbContext)
		{
			_auctionHunterDbContext = auctionHunterDbContext;
		}

		public async Task OnGetAsync()
		{
			PageNumber = PageNumber ?? 1;
			ItemCount = await _auctionHunterDbContext.AuctionHunterItems.CountAsync();
			AuctionHunterItems = await _auctionHunterDbContext.AuctionHunterItems
				.Skip(ItemsPerPage * ((int)PageNumber - 1))
				.Take(ItemsPerPage)
				.ToListAsync();
		}

		public JToken JsonParse(string json)
		{
			return JObject.Parse(json);
		}
	}
}
