using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC_Static_View
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /*忽略url为空的：即www.xxx.com/后面为空  的情况 */
            /*所以根目录下index.html可*/
            //routes.IgnoreRoute("");

            //routes.MapRoute(
            //    name: "html",
            //    url: "{action}.html",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    namespaces: new[] { "MVC_Static_View.Controllers" }
            //    );

            routes.MapRoute(
                "html", // Route name
                "{action}.html", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                new[] { "MVC_Static_View.Controllers" }//默认命名空间
            );
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
