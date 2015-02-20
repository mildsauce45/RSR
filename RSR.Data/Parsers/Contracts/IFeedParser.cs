using System.Collections.Generic;
using System.Xml.Linq;
using RSR.Data.Models;

namespace RSR.Data.Parsers.Contracts
{
	public interface IFeedParser
	{
		IEnumerable<FeedItem> ParseFeed(XElement xml);
	}
}
