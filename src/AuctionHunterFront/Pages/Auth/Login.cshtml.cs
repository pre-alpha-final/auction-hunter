using AuctionHunterFront.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace AuctionHunterFront.Pages.Auth
{
	public class LoginModel : PageModel
    {
		private readonly SignInManager<ApplicationUser> _signInManager;

		[Required]
        [BindProperty]
		public string Username { get; set; }

		[Required]
		[DataType(DataType.Password)]
        [BindProperty]
		public string Password { get; set; }

		public string ReturnUrl { get; set; }

		public LoginModel(SignInManager<ApplicationUser> signInManager)
		{
			_signInManager = signInManager;
		}

		public Task OnGetAsync()
        {
			// Dummy user
			//var result = await _userManager.CreateAsync(new ApplicationUser { UserName = "username" }, "password");

			return Task.CompletedTask;
        }

		[ValidateAntiForgeryToken]
		public async Task<IActionResult> OnPostAsync(string returnUrl = "/")
		{
			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(Username, Password, false, false);
				if (result.Succeeded)
				{
					return LocalRedirect(returnUrl);
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Invalid login attempt");
					return Page();
				}
			}

			return Page();
		}
	}
}