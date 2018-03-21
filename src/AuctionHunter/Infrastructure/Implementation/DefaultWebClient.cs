using AuctionHunter.Results;
using System;
using System.Threading.Tasks;
using AuctionHunter.Controls;

namespace AuctionHunter.Infrastructure.Implementation
{
	public class DefaultWebClient : IWebClient
	{
		private readonly CookieAwareWebClient _webClient = new CookieAwareWebClient();

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

		private void GetContent(string url, WebClientResult webClientResult)
		{
			try
			{
				_webClient.Headers["User-Agent"] =
					"Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:58.0) Gecko/20100101 Firefox/58.0";
				webClientResult.Content = _webClient.DownloadString(url);
				webClientResult.DebugInfo += "Complete\n";
				webClientResult.Success = true;
			}
			catch (Exception e)
			{
				webClientResult.DebugInfo += $"{e.Message}\n";
			}
		}
	}
}
