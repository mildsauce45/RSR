using System.Collections.Generic;
using System.Linq;
using RSR.Data.Models;
using RSR.Data.Repositories.Contracts;

namespace RSR.Data.Repositories
{
	public class SubscriptionRepository : MassiveRespositoryBase<Subscription, int>, ISubscriptionRepository
	{
		public SubscriptionRepository()
			: base("Reader", "Subscriptions", "Id")
		{
		}

		public IEnumerable<Subscription> GetForUser(int userId)
		{
			return this.Find("UserId = @0", args: userId);
		}

		public bool UserHasSubscription(int userId, int subscriptionId)
		{
			return this.Find("UserId = @0 and Id = @1", "*", userId, subscriptionId).Any();
		}

		public override Subscription ToConceptualModel(dynamic row)
		{
			var sub = new Subscription();

			sub.Id = row.Id;
			sub.UserId = row.UserId;
			sub.Name = row.Name;
			sub.XmlUrl = row.XmlUrl;

			return sub;
		}
	}
}
