using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AuctionHunterFront.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AuctionHunterFront.Pages
{
	[Authorize]
	public class IndexModel : PageModel
	{
		private readonly AuctionHunterDbContext _auctionHunterDbContext;

		public IList<AuctionHunterItem> AuctionHunterItems { get; set; }
		public bool HasAuctionHunterItems => AuctionHunterItems?.Count > 0;

		public IndexModel(AuctionHunterDbContext auctionHunterDbContext)
		{
			_auctionHunterDbContext = auctionHunterDbContext;
		}

		public async Task OnGetAsync()
		{
			AuctionHunterItems = await _auctionHunterDbContext.AuctionHunterItems.ToListAsync();
		}

		public JToken JsonParse(string json)
		{
			return JObject.Parse(json);
		}
	}
}
