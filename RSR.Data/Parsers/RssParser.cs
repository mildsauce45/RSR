using System.Collections.Generic;
using System.Xml.Linq;
using RSR.Core.Extensions;
using RSR.Data.Models;
using RSR.Data.Parsers.Contracts;

namespace RSR.Data.Parsers
{
	public class RssParser : IFeedParser
	{
		private bool IsVersion2 { get; set; }

		public IEnumerable<FeedItem> ParseFeed(XElement xml)
		{
			var results = new List<FeedItem>();

			IsVersion2 = xml.Having(x => x.Attribute("version")).If(x => x.Value == "2.0").Return(x => true);

			var channel = xml.Element("channel");
			var baseLink = channel.Element("link").Value;

			foreach (var item in channel.Elements("item"))
				results.Add(this.ToConceptualModel(item));

			return results;
		}

		private FeedItem ToConceptualModel(XElement item)
		{
			var feedItem = new FeedItem();

			feedItem.Title = item.Having(x => x.Element("title")).Return(x => x.Value);
			feedItem.Link = item.Having(x => x.Element("link")).Return(x => x.Value);
			feedItem.Description = item.Having(x => x.Element("description")).Return(x => x.Value);
			feedItem.Author = item.Having(x => x.Element("author")).Return(x => x.Value);
			feedItem.Guid = item.Having(x => x.Element("guid")).Return(x => x.Value);

			return feedItem;
		}
	}
}
