using AuctionHunterFront.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace AuctionHunterFront.Pages.Update
{
	[Authorize]
	public class RunModel : PageModel
    {
		private readonly AuctionHunterDbContext _auctionHunterDbContext;

		[BindProperty(SupportsGet = true)]
		public int? PageNumber { get; set; }

		[BindProperty(SupportsGet = true)]
		public int? MaxPage { get; set; }


		public RunModel(AuctionHunterDbContext auctionHunterDbContext)
		{
			_auctionHunterDbContext = auctionHunterDbContext;
		}

		public async Task<IActionResult> OnGetContinuousPullAsync()
		{
			await Task.Delay(1000);

			_auctionHunterDbContext.Add(new AuctionHunterItem
			{
				AuctionLink = "google.com",
			});
			await _auctionHunterDbContext.SaveChangesAsync();

			return Page();
		}
	}
}