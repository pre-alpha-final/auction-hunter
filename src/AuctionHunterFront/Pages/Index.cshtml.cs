using AuctionHunter.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace AuctionHunterFront.Pages
{
	public class IndexModel : PageModel
    {
		public IList<AuctionItem> DummyData { get; set; } = new List<AuctionItem>
		{
			new AuctionItem { AuctionLink = "google.com", OnPage = 1, Content = JObject.Parse(@"{""name"": ""Some item name""}"), MarkedAsRead = false, Timestamp = DateTime.Now },
		};

		public bool HasDummyData => DummyData.Count > 0;

        public void OnGet()
        {
        }
    }
}
