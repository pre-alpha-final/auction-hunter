using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace AuctionHunterFront.Pages.Update
{
	[Authorize]
	public class RunModel : PageModel
	{
		[BindProperty(SupportsGet = true)]
		public int? PageNumber { get; set; }

		public string DebugInfo { get; set; }

		public async Task<IActionResult> OnGetAsync()
		{
			//var pageResult = await _auctionHunterService.GetItems(PageNumber ?? -1);
			//foreach (var auctionItem in pageResult.AuctionItems.ToList())
			//{
			//	await _auctionHunterService.TryAddAsync(_auctionHunterDbContext, auctionItem);
			//}
			//await _auctionHunterDbContext.SafeSaveChangesAsync();
			//DebugInfo = pageResult.DebugInfo.Replace("\n", "<br />");

			await Task.Delay(500);
			DebugInfo = $"Request for page {PageNumber}<br />Functionality disabled - queries changed to automatic<br />";

			return Page();
		}
	}
}
