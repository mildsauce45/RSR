using System.Collections.Generic;
using RSR.Data.Models;

namespace RSR.Data.Repositories.Contracts
{
	public interface IFeedItemRepository
	{
		IEnumerable<FeedItem> GetFeedItems(int subscriptionId);
	}
}
