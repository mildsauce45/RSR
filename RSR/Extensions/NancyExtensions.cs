using Nancy;
using RSR.Support;

namespace RSR.Extensions
{
	public static class NancyExtensions
	{
		public static int? CurrentUserId(this NancyContext context)
		{
			if (context == null)
				return null;

			var currentUser = context.CurrentUser as UserIdentity;

			if (currentUser != null)
				return currentUser.UserId;

			return null;
		}

		public static int? CurrentUserId(this NancyModule module)
		{
			if (module == null)
				return null;

			return CurrentUserId(module.Context);
		}
	}
}