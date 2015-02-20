using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace RSR.Support
{
	public class Bootstrapper : DefaultNancyBootstrapper
	{
		protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
		{
			base.ConfigureRequestContainer(container, context);
			container.Register<IUserMapper, AuthenticationHandler>();
		}

		protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
		{
			base.RequestStartup(container, pipelines, context);

			var formsAuthConfiguration = new FormsAuthenticationConfiguration
			{
				RedirectUrl = "~/",
				UserMapper = container.Resolve<IUserMapper>()
			};

			FormsAuthentication.Enable(pipelines, formsAuthConfiguration);
		}
	}
}