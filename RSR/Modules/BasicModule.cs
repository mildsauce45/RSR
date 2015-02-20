using Nancy;
using Nancy.Authentication.Forms;
using Nancy.ModelBinding;
using RSR.Support;
using RSR.Support.Contracts;

namespace RSR.Modules
{
	public class BasicModule : NancyModule
	{
		private readonly IAuthenticationHandler authenticationHandler;

		public BasicModule(IAuthenticationHandler authenticationHandler)
		{
			this.authenticationHandler = authenticationHandler;

			Get["/"] = _ =>
			{
				return View["index"];
			};

			Post["/login"] = _ =>
			{
				var input = this.Bind<LoginRequest>();

				var userGuid = authenticationHandler.ValidateUser(input.userName, input.password);

				if (userGuid == null)
					return Response.AsJson(new { success = false });

				var authResult = this.LoginWithoutRedirect(userGuid.Value);

				return Response.AsJson(new { success = true, cookie = authResult.Cookies[0].Value });
			};
		}
	}
}