namespace AuctionHunterFront.Models
{
	public class ApplicationUserAuctionHunterItem
	{
		public string ApplicationUserId { get; set; }

		public int AuctionHunterItemId { get; set; }
		public AuctionHunterItem AuctionHunterItem { get; set; }
	}
}
