using System;
using System.Net;

namespace AuctionHunter.Infrastructure.Implementation
{
	public class DefaultWebClient : IWebClient
	{
		public string Get(string url)
		{
			using (WebClient client = new WebClient())
			{
				client.Headers["User-Agent"] =
					"Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
					"(compatible; MSIE 6.0; Windows NT 5.1; " +
					".NET CLR 1.1.4322; .NET CLR 2.0.50727)";

				return client.DownloadString(url);
			}
		}
	}
}
