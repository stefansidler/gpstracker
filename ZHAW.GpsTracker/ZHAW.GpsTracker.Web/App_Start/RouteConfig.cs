using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ZHAW.GpsTracker.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Session",
                url: "Session/{sessionKey}",
                defaults: new
                {
                    controller = "Session", 
                    action = "Index", 
                    sessionKey = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new
                {
                    controller = "Home", 
                    action = "Index"
                }
            );
        }
    }
}
