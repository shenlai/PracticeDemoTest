using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/*home下静态页，直接走相对路径即可访问，不许要自定义module处理*/
namespace MVC_StaticView.MyHandler
{
    /*http://blog.csdn.net/lubingda/article/details/7102774*/
    /*自定义的管道处理类  webconfig中加入<add name="MvcTest.MyHandler" type="MvcTest.MyHandler"/>才会运行*/
    public class MyHandler : IHttpModule
    {
        #region
        /*
         * Net也提供了一套机制来开发自定义的HttpHandler和 HttpModule，
         * 均可以用于对HttpRequest的截取，完成自定义的处理。 
         * HttpModule 继承System.Web.IHttpModule接口，实现自己的HttpModule类。
         * 必须要实现接口的两个方法：Init和Dispose。在 Init中，可以添加需要截取的事件；
         * Dispose用于资源的释放，如果在Init中创建了自己的资源对象，请在Dispose中进行释放。
         */
        #endregion

        public void Init(HttpApplication context)
        {
            //context.BeginRequest += Application_BeginRequest;
            //context.EndRequest += Application_EndRequest;
            #region
            context.BeginRequest += (new EventHandler(this.Application_BeginRequest));
            context.EndRequest += (new EventHandler(this.Application_EndRequest));
            #endregion
        }
        private void Application_BeginRequest(Object source,
        EventArgs e)
        {
            // Create HttpApplication and HttpContext objects to access
            // request and response properties.
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
            string filePath = context.Request.FilePath;
            string fileExtension =
                VirtualPathUtility.GetExtension(filePath);
            if (fileExtension.Equals(".html"))
            {
                //context.Response.WriteFile(context.Server.MapPath(filePath));//直接走静态页面

                var path = context.Server.MapPath(filePath);
                /*********************避免输出html代码***********************/
                //filePath = string.Format("/Views{0}", filePath);
                context.Response.ContentType = "text/html";
                context.Response.WriteFile(filePath);
                //context.Response.WriteFile(context.Server.MapPath(filePath));//直接走静态页面
                //此处可以加入缓存，条件也可以根据需要来自己定义
                context.Response.End();
            }

        }

        
        //public void Application_BeginRequest(object sender, EventArgs e)
        //{
        //    HttpApplication application = (HttpApplication)sender;
        //    HttpContext context = application.Context;

        //    string filePath = context.Request.FilePath;
        //    string fileExtension = VirtualPathUtility.GetExtension(filePath);//**
        //    if (fileExtension.Equals(".html"))
        //    {
        //        //判断缓存是否存在，不存在加入缓存，调用生成静态的类和方法
        //        //产品过期，移除静态文件，302重定向
        //        if (System.IO.File.Exists(context.Server.MapPath(filePath)))
        //        {
        //            //context.Response.WriteFile(context.Server.MapPath(filePath));//直接走静态页面   :输出html内容，行不通
        //            //context.Response.Redirect(string.Format("http://localhost:38612{0}", filePath),true); /*死循环*/
        //            //context.Response.ContentType = "html/text";
        //            context.Response.Write(context.Server.MapPath(filePath));
        //            //context.RewritePath(filePath);  //修改context.Request.FilePath值
        //            context.Response.End();
        //        }

        //    }

        //}

        public void Application_EndRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;
            string filePath = context.Request.FilePath;
            string fileExtension = VirtualPathUtility.GetExtension(filePath);
            if (fileExtension.Equals(".html"))
            {
                //context.Response.Write("<hr><h1><font color=red>" +
                //    "HelloWorldModule: End of Request</font></h1>");
            }

        }
        public void Dispose()
        {
        }

    }
}