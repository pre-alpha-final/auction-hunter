using System.Threading.Tasks;

namespace AuctionHunterFront.Services
{
	public interface IEmailSender
	{
		Task SendEmailAsync(string email, string subject, string message);
	}
}
