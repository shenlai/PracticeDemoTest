using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebServiceClient
{
    [XmlRoot(ElementName = "GetCtripSubHotelInfo", Namespace = "http://www.opentravel.org/OTA/2003/05")]
    public class GetCtripSubHotelInfo123
    {
        public string masterHotelId { get; set; }
    }
}
