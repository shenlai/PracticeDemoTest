
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace MVC_StaticView.Controllers
{
    /*http://www.tuicool.com/articles/AVbeQb*/
    //http://www.cnblogs.com/keyindex/archive/2012/08/11/2634005.html
    public class HomeController : Controller
    {
        public class TestClass
        {
            [JsonIgnore]
            public string attsJsonSerilizertring { get; set; }
            [ScriptIgnore]
            public string attsScriptSerilizertring { get; set; }
            public int attint { get; set; }
            public double attdouble { get; set; }

            public DateTime atttime { get; set; }
        }
        public ActionResult Index()
        {
            var model = new TestClass() { attdouble = 1.2323, attint = 100, attsJsonSerilizertring = "Json.Net测试序列化", attsScriptSerilizertring = "JavaScriptSerializer测试序列化", atttime = DateTime.Now };
            var s = Json(model);


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult Test()
        {
            return Json(null);
        }

        public ActionResult ECharts()
        {
            return View();
        }
    }
}