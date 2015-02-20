using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml.Linq;
using RSR.Data.Models;
using RSR.Data.Parsers;
using RSR.Data.Parsers.Contracts;
using RSR.Data.Repositories.Contracts;

namespace RSR.Data.Repositories
{
	public class FeedItemRepository : IFeedItemRepository
	{
		private readonly ISubscriptionRepository subscriptionRepository;

		public FeedItemRepository(ISubscriptionRepository subscriptionRepository)
		{
			this.subscriptionRepository = subscriptionRepository;
		}

		public IEnumerable<FeedItem> GetFeedItems(int subscriptionId)
		{
			var sub = subscriptionRepository.Get(subscriptionId);

			var results = new List<FeedItem>();

			if (sub != null)
			{
				try
				{
					var request = HttpWebRequest.Create(sub.XmlUrl);
					var response = request.GetResponse();

					var xmlString = new StreamReader(response.GetResponseStream()).ReadToEnd();

					var xml = XElement.Parse(xmlString);

					var parser = CreateParser(xml);
					if (parser != null)
						results.AddRange(parser.ParseFeed(xml));
				}
				catch
				{
				}
			}

			return results;
		}

		private IFeedParser CreateParser(XElement xml)
		{
			return xml.Name == "rss" ? (IFeedParser)new RssParser() : (IFeedParser)new AtomParser();
		}
	}
}
