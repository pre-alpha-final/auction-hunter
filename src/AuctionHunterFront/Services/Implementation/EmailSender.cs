using System;
using System.Threading.Tasks;

namespace AuctionHunterFront.Services.Implementation
{
	public class EmailSender : IEmailSender
	{
		public Task SendEmailAsync(string email, string subject, string message)
		{
			return Task.CompletedTask;
		}

		public Task SendEmailConfirmationAsync(object email, object callbackUrl)
		{
			return Task.CompletedTask;
		}

		public Task SendResetPasswordAsync(string email, object callbackUrl)
		{
			return Task.CompletedTask;
		}
	}
}
