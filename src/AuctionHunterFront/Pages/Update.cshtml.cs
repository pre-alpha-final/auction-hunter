using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionHunterFront.Pages
{
	public class UpdateModel : PageModel
	{
		private int _currentPage = 1;

		[BindProperty(SupportsGet = true)]
		public int? NumberOfPages { get; set; }

		public void OnGet()
		{
			NumberOfPages = NumberOfPages ?? 1;
		}

		public async Task<JsonResult> OnGetContinuousPullAsync()
		{
			await Task.Delay(5000);
			List<string> lstString = new List<string>
			{
				"Val 1",
				"Val 2",
				"Val 3"
			};
			return new JsonResult(lstString);
		}
	}
}