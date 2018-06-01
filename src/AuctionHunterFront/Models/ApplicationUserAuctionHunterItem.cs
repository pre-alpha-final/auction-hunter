namespace AuctionHunterFront.Models
{
	public class ApplicationUserAuctionHunterItem
	{
		public string ApplicationUserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }

		public string AuctionHunterItemId { get; set; }
		public AuctionHunterItem AuctionHunterItem { get; set; }
	}
}
