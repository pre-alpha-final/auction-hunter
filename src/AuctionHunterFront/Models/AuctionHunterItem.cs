using System;

namespace AuctionHunterFront.Models
{
	public class AuctionHunterItem
	{
		public int ID { get; set; }
		public string AuctionLink { get; set; }
		public int OnPage { get; set; }
		public bool MarkedAsRead { get; set; }
		public DateTime Timestamp { get; set; }
		public string ContentJson { get; set; }
	}
}
