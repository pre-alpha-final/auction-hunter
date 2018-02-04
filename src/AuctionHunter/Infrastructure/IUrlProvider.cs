namespace AuctionHunter.Infrastructure
{
	public interface IUrlProvider
	{
		string BaseUrl { get; set; }
		string GetNextUrl();
	}
}
