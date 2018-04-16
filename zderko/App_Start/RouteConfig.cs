using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace zderko
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Dishes",
                url: "Restaurant/{id}/Dishes",
                defaults: new
                {
                    controller = "Restaurant",
                    action = "Dishes",
                    id = UrlParameter.Optional
                });

            routes.MapRoute(
         name: "Default",
         url: "{controller}/{action}/{id}",
         defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
     );

        }
    }
}
