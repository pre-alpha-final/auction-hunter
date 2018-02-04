using System.Collections.Generic;

namespace AuctionHunter.Infrastructure
{
	public interface IItemsExtractor
	{
		IList<string> GetItems(string page);
	}
}
