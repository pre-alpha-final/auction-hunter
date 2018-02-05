using System;
using Newtonsoft.Json.Linq;

namespace AuctionHunter.Infrastructure.Serializers
{
	public class AuctionItemShow
	{
		public string AuctionLink { get; set; }
		public int OnPage { get; set; }
		public JToken Content { get; set; }
	}
}
