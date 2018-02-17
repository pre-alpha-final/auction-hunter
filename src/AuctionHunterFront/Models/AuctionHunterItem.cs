using System;
using System.Collections.Generic;

namespace AuctionHunterFront.Models
{
	public class AuctionHunterItem
	{
		public int ID { get; set; }
		public string AuctionLink { get; set; }
		public int OnPage { get; set; }
		public string AdditionalDataJson { get; set; }
		public bool MarkedAsRead { get; set; }
		public DateTime Timestamp { get; set; }
	}
}
