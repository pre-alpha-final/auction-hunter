﻿using System;
using System.Threading.Tasks;

namespace AuctionHunterFront.Services.Implementation
{
	public class EmailSender : IEmailSender
	{
		public Task SendEmailAsync(string email, string subject, string message)
		{
			return Task.CompletedTask;
		}
	}
}
