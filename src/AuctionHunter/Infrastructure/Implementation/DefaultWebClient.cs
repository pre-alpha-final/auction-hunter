using System;
using System.Net;
using System.Threading.Tasks;

namespace AuctionHunter.Infrastructure.Implementation
{
	public class DefaultWebClient : IWebClient
	{
		public async Task<string> Get(string url)
		{
			for (var i = 0; i < 10; i++)
			{
				var contents = GetContents(url);
				if (contents != null)
					return contents;
				await Task.Delay(1000);
				Console.WriteLine("retrying...");
			}

			return string.Empty;
		}

		private static string GetContents(string url)
		{
			try
			{
				using (var client = new WebClient())
				{
					client.Headers["User-Agent"] =
						"Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
						"(compatible; MSIE 6.0; Windows NT 5.1; " +
						".NET CLR 1.1.4322; .NET CLR 2.0.50727)";

					return client.DownloadString(url);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

			return null;
		}
	}
}
