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

		public string Text { get; set; }

		public RunModel()
		{
			
		}

		public async Task<IActionResult> OnGetContinuousPullAsync()
		{
			await Task.Delay(100);
			Text = "Line one\nLine two";
			Text = Text.Replace("\n", "<br/>");
			return Page();
		}
	}
}