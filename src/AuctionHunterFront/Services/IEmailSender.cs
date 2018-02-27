using System.Threading.Tasks;

namespace AuctionHunterFront.Services
{
	public interface IEmailSender
	{
		Task SendEmailAsync(string email, string subject, string message);
		Task SendEmailConfirmationAsync(object email, object callbackUrl);
		Task SendResetPasswordAsync(string email, object callbackUrl);
	}
}
