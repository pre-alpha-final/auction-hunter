using HtmlAgilityPack;
using System;

namespace AuctionHunter.Extensions
{
	public static class HtmlNodeExtensions
	{
		public static HtmlNodeCollection SafeSelectNodes(this HtmlNode htmlNode, string xpath)
		{
			try
			{
				return htmlNode.SelectNodes(xpath);
			}
			catch (Exception e)
			{
				// ignore
			}

			return null;
		}
	}
}
