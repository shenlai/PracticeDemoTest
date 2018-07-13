using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Static_View.Handler
{
    public class MyHandler : IHttpModule
    {
        public void Init(HttpApplication application)
        {
            application.BeginRequest +=
            (new EventHandler(this.Application_BeginRequest));
            application.EndRequest +=
            (new EventHandler(this.Application_EndRequest));
        }
        private void Application_BeginRequest(Object source,
        EventArgs e)
        {
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
            if (context.Request.FilePath == "/")
                context.RewritePath("index.html");
            /*
            // Create HttpApplication and HttpContext objects to access 
            // request and response properties. 
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
            string filePath = context.Request.FilePath;
            string fileExtension =
            VirtualPathUtility.GetExtension(filePath);
            if (fileExtension.Equals(".html"))
            {
                context.Response.WriteFile(context.Server.MapPath(filePath));//直接走静态页面 
                //此处可以加入缓存，条件也可以根据需要来自己定义 
                context.Response.End();
            }
             * */
        }
        private void Application_EndRequest(Object source, EventArgs e)
        {
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
            string filePath = context.Request.FilePath;
            string fileExtension =
            VirtualPathUtility.GetExtension(filePath);
            //if (fileExtension.Equals(".html"))
            //{
            //    context.Response.Write("<hr><h1><font color=red>" +
            //    "HelloWorldModule: End of Request</font></h1>");
            //}
        }
        public void Dispose() { }
    }
}