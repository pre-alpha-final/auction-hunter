using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace AuctionHunterFront.Pages.Update
{
	public class RunModel : PageModel
    {
		[BindProperty(SupportsGet = true)]
		public int? PageNumber { get; set; }

		[BindProperty(SupportsGet = true)]
		public int? MaxPage { get; set; }

		public async Task<IActionResult> OnGetContinuousPullAsync()
		{
			await Task.Delay(100);
			return Page();
		}
	}
}