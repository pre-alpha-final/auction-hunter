using AuctionHunter.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace AuctionHunterFront.Pages
{
	public class IndexModel : PageModel
    {
		public IList<AuctionItem> DummyData { get; set; } = new List<AuctionItem>
		{
			new AuctionItem { AuctionLink = "google.com", OnPage = 1, Content = @"{'prop1', 'value1'}", MarkedAsRead = false, Timestamp = DateTime.Now },
		};

		public bool HasDummyData => DummyData.Count > 0;

        public void OnGet()
        {

        }
    }
}
