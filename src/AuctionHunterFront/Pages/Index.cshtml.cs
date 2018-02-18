using AuctionHunter.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AuctionHunterFront.Pages
{
	[Authorize]
	public class IndexModel : PageModel
	{
		public IList<AuctionItem> DummyData { get; set; } = new List<AuctionItem>
		{
			new AuctionItem { AuctionLink = "google.com", OnPage = 1, ContentJson = @"{""name"": ""Some item name""}", Timestamp = DateTime.Now },
		};

		public bool HasDummyData => DummyData.Count > 0;

		public Task OnGetAsync()
		{
			return Task.CompletedTask;
		}

		public JToken JsonParse(string json)
		{
			return JObject.Parse(json);
		}
	}
}
