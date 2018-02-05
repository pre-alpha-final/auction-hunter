using System;
using Newtonsoft.Json.Linq;

namespace AuctionHunter.Infrastructure.Serializers
{
	public class AuctionItemCache
	{
		public string AuctionLink { get; set; }
		public bool MarkedAsRead { get; set; }
		public DateTime Timestamp { get; set; }
	}
}
