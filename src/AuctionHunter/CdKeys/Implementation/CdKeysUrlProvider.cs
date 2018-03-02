using AuctionHunter.Infrastructure;

namespace AuctionHunter.G2A.Implementation
{
	public class G2AUrlProvider : IUrlProvider
	{
		public string BaseUrl { get; set; }

		public string GetUrlForPage(int pageNumber)
		{
			return BaseUrl.Replace("page=", $"page={pageNumber}");
		}
	}
}
