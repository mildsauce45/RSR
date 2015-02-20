using System.Collections.Generic;
using RSR.Data.Models;

namespace RSR.Data.Repositories.Contracts
{
	public interface ISubscriptionRepository : IMassiveRepositoryBase<Subscription, int>
	{
		IEnumerable<Subscription> GetForUser(int userId);
		bool UserHasSubscription(int userId, int subscriptionId);
	}
}
