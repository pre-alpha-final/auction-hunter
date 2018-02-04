using Newtonsoft.Json.Linq;
using System;

namespace AuctionHunter.Infrastructure
{
	public class AuctionItem
    {
		public string AuctionLink { get; set; }
		public JToken Content { get; set; }
		public DateTime Timestamp { get; set; }
    }
}
