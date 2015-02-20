using System.Linq;
using Nancy;
using Nancy.Security;
using RSR.Data.Models;
using RSR.Data.Repositories.Contracts;
using RSR.Extensions;

namespace RSR.Modules
{
	public class SubscriptionModule : NancyModule
	{
		private readonly ISubscriptionRepository subscriptionRepository;
		private readonly IFeedItemRepository feedItemRepository;

		public SubscriptionModule(ISubscriptionRepository subscriptionRepository, IFeedItemRepository feedItemRepository)
			: base("subscriptions")
		{
			this.subscriptionRepository = subscriptionRepository;
			this.feedItemRepository = feedItemRepository;

			this.RequiresAuthentication();

			Get["/"] = _ =>
			{
				var userId = this.CurrentUserId();

				if (userId.HasValue)
				{
					var subs = subscriptionRepository.GetForUser(userId.Value).ToList();

					return Response.AsJson(subs);
				}

				return Response.AsJson(Enumerable.Empty<Subscription>());
			};

			Get["/{subId}"] = parameters =>
			{
				var userId = this.CurrentUserId();

				if (!userId.HasValue)
					return Response.AsJson(Enumerable.Empty<FeedItem>());

				// Permissions checking
				if (subscriptionRepository.UserHasSubscription(userId.Value, parameters.subId))
				{
					var feedItems = feedItemRepository.GetFeedItems((int)parameters.subId).ToList();

					return Response.AsJson(feedItems);
				}

				return Response.AsJson(Enumerable.Empty<FeedItem>());
			};			
		}
	}
}