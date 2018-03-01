namespace AuctionHunterFront.Models
{
	public class ApplicationUserAuctionHunterItem
    {
		public int Id { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
		public AuctionHunterItem AuctionHunterItem { get; set; }
	}
}
