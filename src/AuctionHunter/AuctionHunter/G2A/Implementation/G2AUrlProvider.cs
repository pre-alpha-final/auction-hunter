using AuctionHunter.Infrastructure;

namespace AuctionHunter.G2A.Implementation
{
	public class G2AUrlProvider : IUrlProvider
	{
		private int _page = 1;

		public string BaseUrl { get; set; }

		public string GetNextUrl()
		{
			return BaseUrl.Replace("page=", $"page={_page++}");
		}
	}
}
