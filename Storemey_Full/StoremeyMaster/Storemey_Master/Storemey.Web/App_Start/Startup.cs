using System;
using System.Configuration;
using Abp.Owin;
using Storemey.Api.Controllers;
using Storemey.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System.Web.Optimization;

[assembly: OwinStartup(typeof(Startup))]

namespace Storemey.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseAbp();

            app.UseOAuthBearerAuthentication(AccountController.OAuthBearerOptions);

            StoremeyConsts.CookieName = Guid.NewGuid().ToString();
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                CookieName = StoremeyConsts.CookieName,
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Login"),
                // evaluate for Persistent cookies (IsPermanent == true). Defaults to 14 days when not set.
                ExpireTimeSpan = TimeSpan.FromMinutes(120),//new TimeSpan(int.Parse(ConfigurationManager.AppSettings["AuthSession.ExpireTimeInDays.WhenPersistent"] ?? "1440"), 1440, 0, 0),
                SlidingExpiration = bool.Parse(ConfigurationManager.AppSettings["AuthSession.SlidingExpirationEnabled"] ?? bool.FalseString),
                CookieDomain = "." + ConfigurationManager.AppSettings["DomainName"],
                Provider = new CookieAuthenticationProvider
                {
                    OnResponseSignIn = context =>
                    {
                        context.Properties.AllowRefresh = true;
                        context.Properties.ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(120);
                    }
                }

            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.MapSignalR();

            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
    }
}
