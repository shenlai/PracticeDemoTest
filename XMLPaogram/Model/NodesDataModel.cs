using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLPaogram.Model
{
    /// <summary>
    /// 类名 必须与 XML中父节点同名
    /// </summary>
    public class AppSetting
    {
        [XmlElement("App")]
        public List<App> list { get; set; }
    }
    public class App
    {
        
        public string AppName{get;set;}
        public string Version { get; set; }
        public string Url{get;set;}
        public string Isforce{get;set;}
        public string UpdateContent{get;set;}
    }
}
