using AuctionHunter.Infrastructure;
using System.Collections.Generic;

namespace AuctionHunter.Results
{
	public class PageResult
	{
		public IList<AuctionItem> AuctionItems { get; set; } = new List<AuctionItem>();
		public string DebugInfo { get; set; }
		public bool Success { get; set; }
	}
}
