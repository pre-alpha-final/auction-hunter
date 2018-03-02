﻿using AuctionHunter.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace AuctionHunter.CdKeys.Implementation
{
	public static class CdKeysDefaults
	{
		public static string DefaultBaseUrl => "https://www.g2a.com/new/api/products/filter?category_id=games&changeType=PAGINATION&currency=PLN&min_price[max]=100&min_price[min]=0&page=&platform=1&store=polish";
		public static ICdKeysUrlProvider DefaultUrlProvider => Program.Container.GetService<ICdKeysUrlProvider>();
		public static IWebClient DefaultWebCllient => Program.Container.GetService<IWebClient>();
		public static ICdKeysItemsExtractor DefaultItemsExtractor => Program.Container.GetService<ICdKeysItemsExtractor>();
		public static ICdKeysAuctionLinkExtractor DefaultAuctionLinkExtractor => Program.Container.GetService<ICdKeysAuctionLinkExtractor>();
		public static ICdKeysContentExtractor DefaultContentExtractor => Program.Container.GetService<ICdKeysContentExtractor>();
	}
}
