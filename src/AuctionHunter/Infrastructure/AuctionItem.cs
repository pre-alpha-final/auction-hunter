using System;

namespace AuctionHunter.Infrastructure
{
	public class AuctionItem
	{
		public string AuctionLink { get; set; }
		public int OnPage { get; set; }
		public DateTime Timestamp { get; set; }
		public string ContentJson { get; set; }
	}
}
