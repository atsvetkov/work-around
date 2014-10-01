using System;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using WorkAround.Data;

namespace WorkAround.Web
{
	public class Startup
	{
		public static Func<UserManager<AppUser>> UserManagerFactory { get; private set; }

		public void Configuration(IAppBuilder app)
		{
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = "ApplicationCookie",
				LoginPath = new PathString("/auth/login")
			});

			UserManagerFactory = () =>
			{
				var userManager = new UserManager<AppUser>(new UserStore<AppUser>(new AppDbContext()));
				
				userManager.UserValidator = new UserValidator<AppUser>(userManager)
				{
					AllowOnlyAlphanumericUserNames = false
				};

				userManager.ClaimsIdentityFactory = new AppUserClaimsIdentityFactory();

				return userManager;
			};

			var config = new HttpConfiguration();
			config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{k}/{l}",
                defaults: new { k = RouteParameter.Optional, l = RouteParameter.Optional }
            );

			app.UseWebApi(config);
		}
	}
}