using AuctionHunter.Extensions;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Globalization;
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

			htmlNodeCollection = htmlDocument.DocumentNode.SafeSelectNodes("//span[@class='price']");
			var priceTags = htmlNodeCollection
				.Select(e => e.InnerHtml.TrimStart().TrimEnd())
				.Where(e => string.IsNullOrWhiteSpace(e) == false)
				.ToList();
			var currency = priceTags.FirstOrDefault()?.Split(' ')[0];
			var price = SelectLowest(priceTags);

			htmlNodeCollection = htmlDocument.DocumentNode.SafeSelectNodes("//img/@src");
			var image = htmlNodeCollection?.FirstOrDefault()?.Attributes["src"]?.Value;

			var content = new JObject(
				new JProperty("name", name),
				new JProperty("price", $"{price} {currency}"),
				new JProperty("image", image));

			if (item.Contains("Out of stock"))
			{
				content.Add(
					new JProperty(
						"extras",
						new JObject(
							new JProperty("Out of stock", true)
						)
					)
				);
			}

			return content;
		}

		private string SelectLowest(List<string> priceTags)
		{
			var prices = priceTags
				.Select(e => decimal.Parse(e.Split(' ')[1], new CultureInfo("en-US")))
				.ToList();

			return prices
				.Where(e => e > 0)
				.Min()
				.ToString(new CultureInfo("en-US"));
		}
	}
}
