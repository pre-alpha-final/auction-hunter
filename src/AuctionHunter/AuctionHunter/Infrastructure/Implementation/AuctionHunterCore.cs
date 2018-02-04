﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
		public ITitleExtractor TitleExtractor { get; set; }
		public IAuctionLinkExtractor AuctionLinkExtractor { get; set; }
		public IList<string> SkipPatterns { get; set; }

		public void Run()
		{
			var allAuctionItems = new List<AuctionItem>();
			for (int i = 1; i <= NumberOfPages; i++)
			{
				Console.WriteLine($"Doing page number: {i}");
				var url = UrlProvider.GetNextUrl();
				var page = WebClient.Get(url);
				var items = ItemsExtractor.GetItems(page);
				allAuctionItems.AddRange(ConvertItems(items.ToList()));
			}

			var savedAuctionItems = Load($"{Name}.cache").ToList();
			var resultAuctionItems = GetResultAuctionItemList(allAuctionItems, savedAuctionItems).ToList();

			Save($"{Name}.cache", savedAuctionItems);
			Save($"{Name}_Results.txt", resultAuctionItems);
		}

		private IList<AuctionItem> ConvertItems(List<string> items)
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

		private IList<AuctionItem> GetResultAuctionItemList(List<AuctionItem> allAuctionItems, List<AuctionItem> savedAuctionItems)
		{
			var resultAuctionItems = new List<AuctionItem>();
			foreach (var item in allAuctionItems)
			{
				var oldItem = savedAuctionItems.Where(e => e.AuctionLink == item.AuctionLink).FirstOrDefault();
				if (oldItem == null)
				{
					resultAuctionItems.Add(item);
					savedAuctionItems.Add(item);
				}
				else if (DateTime.Compare(item.Timestamp, oldItem.Timestamp + TimeSpan.FromDays(NumberOfDays)) < 0)
				{
					resultAuctionItems.Add(item);
				}
			}

			return resultAuctionItems;
		}

		private IList<AuctionItem> Load(string name)
		{
			if (File.Exists(name) == false)
				return new List<AuctionItem>();

			using (StreamReader r = new StreamReader(name))
			{
				var json = r.ReadToEnd();
				return JsonConvert.DeserializeObject<List<AuctionItem>>(json);
			}
		}

		private void Save(string name, List<AuctionItem> items)
		{
			using (StreamWriter w = File.CreateText(name))
			{
				w.Write(JsonConvert.SerializeObject(items, Formatting.Indented));
			}
		}
	}
}