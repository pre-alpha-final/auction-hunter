using AuctionHunter.Results;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AuctionHunter.Infrastructure.Implementation
{
	public class DefaultWebClient : IWebClient
	{
		public async Task<WebClientResult> Get(string url)
		{
			var webClientResult = new WebClientResult();
			for (var i = 0; i < 10; i++)
			{
				GetContent(url, webClientResult);
				if (webClientResult.Success)
				{
					return webClientResult;
				}

				await Task.Delay(1000);
				webClientResult.DebugInfo += "retrying...\n";
			}

			webClientResult.DebugInfo += "Download failed\n";
			return webClientResult;
		}

		private static void GetContent(string url, WebClientResult webClientResult)
		{
			try
			{
				using (var client = new WebClient())
				{
					client.Headers["User-Agent"] =
						"Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
						"(compatible; MSIE 6.0; Windows NT 5.1; " +
						".NET CLR 1.1.4322; .NET CLR 2.0.50727)";

					webClientResult.Content = client.DownloadString(url);
					webClientResult.DebugInfo += "Complete\n";
					webClientResult.Success = true;
				}
			}
			catch (Exception e)
			{
				webClientResult.DebugInfo += $"{e.Message}\n";
			}
		}
	}
}
