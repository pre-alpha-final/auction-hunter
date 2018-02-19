using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace AuctionHunterFront.Pages.Update
{
	[Authorize]
	public class IndexModel : PageModel
	{
		[BindProperty(SupportsGet = true)]
		public int? PageNumber { get; set; }

		[BindProperty(SupportsGet = true)]
		public int? MaxPage { get; set; }

		public Task<IActionResult> OnGetAsync()
		{
			return MaxPage == null
				? Task.FromResult((IActionResult)RedirectPermanent("/Update/1"))
				: Task.FromResult((IActionResult)Page());
		}
	}
}