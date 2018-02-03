using System;
using System.Collections.Generic;
using System.Linq;

namespace AuctionHunter.Infrastructure.Implementation
{
	public class AuctionHunterCore : IAuctionHunterCore
	{
		// Builder filled section
		public string Name { get; set; }
		public int NumberOfPages { get; set; }
		public int NumberOfDays { get; set; }
		public IUrlProvider UrlProvider { get; set; }
		public IWebClient WebClient { get; set; }
		public IItemsExtractor ItemsExtractor { get; set; }
		public ITitleExtractor TitleExtractor { get; set; }
		public IAuctionLinkExtractor AuctionLinkExtractor { get; set; }
		public List<string> SkipPatterns { get; set; }
		// Builder filled section

		private List<AuctionItem> _savedAuctionItems = new List<AuctionItem>();
		private List<AuctionItem> _allAuctionItems = new List<AuctionItem>();
		private List<AuctionItem> _resultAuctionItems = new List<AuctionItem>();

		public void Run()
		{
			for (int i = 1; i <= NumberOfPages; i++)
			{
				Console.WriteLine($"Doing page number: {i}\n");
				var url = UrlProvider.GetNextUrl();
				var page = WebClient.Get(url);
				var items = ItemsExtractor.GetItems(page);
				_allAuctionItems.AddRange(ConvertItems(items));
			}

			_savedAuctionItems = Load($"{Name}.cache");
			foreach(var item in _allAuctionItems)
			{
				var oldItem = _savedAuctionItems.Where(e => e.AuctionLink == item.AuctionLink).FirstOrDefault();
				if (oldItem == null)
				{
					_resultAuctionItems.Add(item);
					_savedAuctionItems.Add(item);
				}
				else if (DateTime.Compare(item.Timestamp, oldItem.Timestamp + TimeSpan.FromDays(NumberOfDays)) > 0)
				{
					_resultAuctionItems.Add(item);
				}
			}
			Save($"{Name}.cache", _savedAuctionItems);
			Save($"{Name}_Results.txt", _resultAuctionItems);
		}

		private List<AuctionItem> ConvertItems(List<AuctionItem> items)
		{
			var convertedItems = new List<AuctionItem>();
			foreach (var item in items)
			{
				var title = TitleExtractor.Extract(item);
				var auctionLink = AuctionLinkExtractor.Extract(item);
				if (SkipPatterns.Any(e => title.Contains(e)))
					continue;
				convertedItems.Add(new AuctionItem
				{
					Title = title,
					AuctionLink = auctionLink,
					Timestamp = DateTime.Now,
				});
			}

			return convertedItems;
		}

		private List<AuctionItem> Load(string name)
		{
			throw new NotImplementedException();
		}

		private void Save(string name, List<AuctionItem> items)
		{
			throw new NotImplementedException();
		}
	}
}
