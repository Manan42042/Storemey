using LowercaseDashedRouting;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Storemey.Web
{
    public static class RouteConfig 
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //ASP.NET Web API Route Config
            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            //routes.MapRoute("SpecificRoute",
            //     "store/{action}/{id}",
            //     new { controller = "Home", action = "SiteMaster", id = UrlParameter.Optional });


            routes.Add(new LowercaseDashedRoute("{action}",
          new RouteValueDictionary(
              new { controller = "Home", action = "SiteMaster"}),
              new DashedRouteHandler()
          )
      );
            routes.MapRoute(
                name: "Download",
                url: "Download/{id}",
                defaults: new { controller = "Home", action = "Download", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "SiteMaster", id = UrlParameter.Optional }
            );



        }
    }

  
}
