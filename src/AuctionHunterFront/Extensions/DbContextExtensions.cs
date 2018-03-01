using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace AuctionHunterFront.Extensions
{
	public static class DbContextExtensions
    {
		public static void Log(this DbContext dbContext, LogLevel logLevel = LogLevel.Information)
		{
			var contextServices = ((IInfrastructure<IServiceProvider>)dbContext).Instance;
			var loggerFactory = contextServices.GetRequiredService<ILoggerFactory>();
			loggerFactory.AddConsole(logLevel);
		}
    }
}
