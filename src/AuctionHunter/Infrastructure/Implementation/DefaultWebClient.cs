using AuctionHunter.Results;
using System;
using System.Threading.Tasks;
using AuctionHunter.Controls;
using AuctionHunter.Extensions;

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
				try
				{
					if (i > 0)
					{
						webClientResult.DebugInfo.AppendLine("retrying...");
					}
					webClientResult.Content = await _webClient.Get(url, true);
					webClientResult.DebugInfo.AppendLine("Complete");
					webClientResult.Success = true;
					return webClientResult;
				}
				catch (Exception e)
				{
					webClientResult.DebugInfo.AppendLine($"{e.Message}");
				}

				await Task.Delay(1000);
			}

			webClientResult.DebugInfo.AppendLine("Download failed");
			return webClientResult;
		}
	}
}
