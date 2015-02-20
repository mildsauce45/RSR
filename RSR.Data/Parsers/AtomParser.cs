using System.Collections.Generic;
using System.Xml.Linq;
using RSR.Core.Extensions;
using RSR.Data.Models;
using RSR.Data.Parsers.Contracts;

namespace RSR.Data.Parsers
{
	public class AtomParser : IFeedParser
	{
		private XNamespace ns = "http://www.w3.org/2005/Atom";

		public IEnumerable<FeedItem> ParseFeed(XElement xml)
		{
			var results = new List<FeedItem>();

			foreach (var entry in xml.Elements(ns + "entry"))
				results.Add(this.ToConceptualModel(entry));

			return results;
		}

		private FeedItem ToConceptualModel(XElement item)
		{
			var feedItem = new FeedItem();

			feedItem.Title = item.Element(ns + "title").Value;
			feedItem.Link = item.Having(x => x.Element(ns + "link")).Having(x => x.Attribute("href")).Return(x => x.Value);
			feedItem.Guid = item.Element(ns + "id").Value;
			feedItem.Description = item.Element(ns + "summary").Value;

			return feedItem;
		}
	}
}
