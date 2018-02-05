using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AuctionHunter.Infrastructure.Serializers;

namespace AuctionHunter.Infrastructure.Implementation
{
	public class AuctionHunterCore : IAuctionHunterCore
	{
		public string Name { get; set; }
		public int NumberOfPages { get; set; }
		public int NumberOfDays { get; set; }
		public IUrlProvider UrlProvider { get; set; }
		public IWebClient WebClient { get; set; }
		public IItemsExtractor ItemsExtractor { get; set; }
		public IAuctionLinkExtractor AuctionLinkExtractor { get; set; }
		public IContentExtractor ContentExtractor { get; set; }
		public IList<string> SkipPatterns { get; set; }

		private bool _initialRun;

		public async Task Run()
		{
			var savedAuctionItems = Load<AuctionItemCache>($"{Name}.cache").ToList();
			if (savedAuctionItems.Count == 0)
				_initialRun = true;

			var allAuctionItems = new List<AuctionItem>();
			for (var i = 1; i <= NumberOfPages; i++)
			{
				Console.WriteLine($"Doing page number: {i}");
				var url = UrlProvider.GetNextUrl();
				var page = await WebClient.Get(url);
				var items = ItemsExtractor.GetItems(page);
				allAuctionItems.AddRange(ConvertItems(i, items.ToList()));
			}
			UpdateLists(allAuctionItems, savedAuctionItems, out var resultAuctionItems);

			Save($"{Name}.cache", savedAuctionItems);
			Save($"{Name}_Results.txt", resultAuctionItems);
		}

		private IEnumerable<AuctionItem> ConvertItems(int pageNumber, IEnumerable<string> items)
		{
			var convertedItems = new List<AuctionItem>();
			foreach (var item in items)
			{
				var auctionLink = AuctionLinkExtractor.Extract(item);
				var content = ContentExtractor.Extract(item);
				if (SkipPatterns.Any(e => content.ToString().Contains(e)))
					continue;
				convertedItems.Add(new AuctionItem
				{
					AuctionLink = auctionLink,
					OnPage = pageNumber,
					Content = content,
                    MarkedAsRead = _initialRun,
					Timestamp = DateTime.Now,
				});
			}

			return convertedItems;
		}

		private void UpdateLists(IEnumerable<AuctionItem> allAuctionItems, ICollection<AuctionItemCache> savedAuctionItems, out List<AuctionItemShow> resultAuctionItems)
		{
			resultAuctionItems = new List<AuctionItemShow>();
			foreach (var item in allAuctionItems)
			{
				var oldItem = savedAuctionItems.FirstOrDefault(e => e.AuctionLink == item.AuctionLink);
				if (oldItem == null)
				{
					resultAuctionItems.Add(new AuctionItemShow
					{
					    AuctionLink = item.AuctionLink,
                        OnPage = item.OnPage,
                        Content = item.Content,
					});
					savedAuctionItems.Add(new AuctionItemCache
					{
					    AuctionLink = item.AuctionLink,
					    MarkedAsRead = item.MarkedAsRead,
					    Timestamp = item.Timestamp,
					});
				}
				else if (oldItem.MarkedAsRead == false && DateTime.Compare(item.Timestamp, oldItem.Timestamp + TimeSpan.FromDays(NumberOfDays)) < 0)
				{
					resultAuctionItems.Add(new AuctionItemShow
					{
					    AuctionLink = item.AuctionLink,
					    OnPage = item.OnPage,
					    Content = item.Content,
					});
				}
			}
		}

		private static IEnumerable<T> Load<T>(string name)
		{
			if (File.Exists(name) == false)
				return new List<T>();

			using (var r = new StreamReader(name))
			{
				var json = r.ReadToEnd();
				return JsonConvert.DeserializeObject<List<T>>(json);
			}
		}

		private static void Save<T>(string name, List<T> items, Formatting formatting = Formatting.Indented)
		{
			using (var w = File.CreateText(name))
			{
				w.Write(JsonConvert.SerializeObject(items, formatting));
			}
		}
	}
}
