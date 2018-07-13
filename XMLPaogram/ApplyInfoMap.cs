using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLPaogram
{
    /*序列化时 不加特性默认都会以XmlElement处理，因此如果是[XmlAttribute]则必须加 */

    [XmlRoot(Namespace = "http://www.opentravel.org/OTA/2003/05")]
    public class ApplyInfoMapModelTest
    {
        [XmlAttribute]
        public string AppKey { get; set; }
        [XmlAttribute]
        public string ApiKey { get; set; }
        [XmlAttribute]
        public string ApiClass { get; set; }
        [XmlAttribute]
        public string ApiList { get; set; }
        [XmlAttribute("Real")]
        public string RealClass { get; set; }
        [XmlElement("Des")]
        public string Desxxx { get; set; }

        [XmlElement("List")]
        public List<string> List { get; set; }

    }

    /// <summary>
    /// 类名ApplyInfoMap 同XML最外层节点名
    /// </summary>
    public class ApplyInfoMap
    {
        public ApplyInfoMap()
        {
            list = new List<ApplyInfoMapModelTest>();
         }
        [XmlElement("add")]
        public List<ApplyInfoMapModelTest> list { get; set; }
    }
}
