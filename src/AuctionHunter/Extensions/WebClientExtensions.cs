using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHunter.Extensions
{
	public static class WebClientExtensions
	{
		public static Task<string> Get(this WebClient webClient, string url, bool throwExceptions = false)
		{
			try
			{
				webClient.Headers["User-Agent"] =
					"Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:58.0) Gecko/20100101 Firefox/58.0";

				var result = webClient.DownloadString(new Uri(url));

				return Task.FromResult(result);
			}
			catch (Exception e)
			{
				if (throwExceptions)
					throw;
			}

			return null;
		}

		public static Task<string> Post(this WebClient webClient, string url, NameValueCollection values, bool throwExceptions = false)
		{
			try
			{
				webClient.Headers["User-Agent"] =
					"Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:58.0) Gecko/20100101 Firefox/58.0";
				webClient.Headers["Content-Type"] =
					"application/x-www-form-urlencoded";

				var result = webClient.UploadValues(url, "POST", values);

				return Task.FromResult(Encoding.UTF8.GetString(result));
			}
			catch (Exception e)
			{
				if (throwExceptions)
					throw;
			}

			return null;
		}
	}
}
