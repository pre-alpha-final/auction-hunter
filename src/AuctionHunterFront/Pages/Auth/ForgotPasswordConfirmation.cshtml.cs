using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuctionHunterFront.Pages.Auth
{
	public class ForgotPasswordConfirmationModel : PageModel
    {
        public Task OnGetAsync()
        {
			return Task.FromResult(Page());
        }
    }
}