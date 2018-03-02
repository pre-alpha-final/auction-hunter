namespace AuctionHunter.G2A.Implementation
{
	public class G2AUrlProvider : IG2AUrlProvider
	{
		public string BaseUrl { get; set; }

		public string GetUrlForPage(int pageNumber)
		{
			return BaseUrl.Replace("page=", $"page={pageNumber}");
		}
	}
}
