using AuctionHunter.Extensions;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace AuctionHunter.CdKeys.Implementation
{
	public class CdKeysContentExtractor : ICdKeysContentExtractor
	{
		public JToken Extract(string item)
		{
			var htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(item);

			var htmlNodeCollection = htmlDocument.DocumentNode.SafeSelectNodes("//a/@href");
			var name = htmlNodeCollection?.FirstOrDefault()?.Attributes["title"]?.Value;

			htmlNodeCollection = htmlDocument.DocumentNode.SafeSelectNodes("//span[@class='price' and @style='display:inline']");
			var price = htmlNodeCollection?.FirstOrDefault()?.InnerHtml;
			if (string.IsNullOrWhiteSpace(price))
			{
				htmlNodeCollection = htmlDocument.DocumentNode.SafeSelectNodes("//span[@class='price']");
				price = htmlNodeCollection?.FirstOrDefault()?.InnerHtml;
			}

			htmlNodeCollection = htmlDocument.DocumentNode.SafeSelectNodes("//img/@src");
			var image = htmlNodeCollection?.FirstOrDefault()?.Attributes["src"]?.Value;

			return new JObject(
				new JProperty("name", name),
				new JProperty("price", $"{price?.Split(' ')[1]} {price?.Split(' ')[0]}"),
				new JProperty("image", image));
		}
	}
}
