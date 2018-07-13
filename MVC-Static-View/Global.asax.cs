using RouteDebug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MVC_Static_View
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
        }
        
        /*更目录下静态index.html页，Views/Home/index.cshtml页*/
        /*实现结果：
         * global中加入Application_BeginRequest方法
         * 路由表加入：
         * routes.MapRoute(
                "html", // Route name
                "{action}.html", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                new[] { "MVC_Static_View.Controllers" }//默认命名空间
            );
         * 
         * 浏览器输入http://localhost:5276/   根目录存在静态index.html则走静态html 不存在则走 /home/index
         * 
         * 当处理一个应用的根URL的请求时，这个URL有时会被服务器代替"/"来传入）。
         * 这个规则确保对我们应用的根"/index.html"或"/"的请求，
         * 都会由HomeController类（是在我们使用asp.net mvc Web Application项目模板生成一个新的应用时，由Visual Studio自动生成的控制器）里的Index() action方法处理
         */
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (Context.Request.FilePath == "/")
            {
                Context.RewritePath("index.html"); //FilePath = "/index.html"
                //Context.Response.WriteFile(Context.Server.MapPath(Context.Request.FilePath));
            }

        }

    }
}
