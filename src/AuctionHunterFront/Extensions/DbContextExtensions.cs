using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

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

		public static async Task<int> SafeSaveChangesAsync(this DbContext dbContext, ILogger logger = null)
		{
			try
			{
				return await dbContext.SaveChangesAsync();
			}
			catch (Exception e)
			{
				var log = $"SafeSaveChangesAsync exception:\n {e}";
				Console.WriteLine(log);
				logger?.LogError(log);
			}

			return -1;
		}
	}
}
