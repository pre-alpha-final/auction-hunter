using AuctionHunter.Infrastructure;
using System;

namespace AuctionHunter.G2A.Implementation
{
	public class G2AUrlProvider : IUrlProvider
	{
		public string BaseUrl { get; set; }

		public string GetNextUrl()
		{
			throw new NotImplementedException();
		}
	}
}
