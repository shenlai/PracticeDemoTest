using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace WebServiceClient
{
    public class HttpWebRequestClient
    {

        public static Hotels SoapV1_2Test()
        {
            string param = @"<?xml version=""1.0"" encoding=""utf-8""?>
                                        <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                                          <soap:Body>
                                            <GetCtripSubHotelInfo xmlns=""http://www.opentravel.org/OTA/2003/05"">
                                                <masterHotelId>121</masterHotelId>
                                            </GetCtripSubHotelInfo >
                                          </soap:Body>
                                        </soap:Envelope>";
            byte[] bs = Encoding.UTF8.GetBytes(param);
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("http://124.127.242.67/automappingwebapi/automappingservices.asmx");

            myRequest.Method = "POST";
            myRequest.ContentType = "text/xml;charset=utf-8";
            myRequest.Headers.Add("SOAPAction", "http://htng.org/2014B/HTNG_ARIAndReservationPushService#GetCtripSubHotelInfo");
            myRequest.ContentLength = bs.Length;

            using (Stream reqStream = myRequest.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
            }
            using (HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse())
            {
                StreamReader mysr = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                string responseResult = mysr.ReadToEnd();
                var hotel = XmlHelper.XmlDeserialize<Hotels>(responseResult, Encoding.UTF8);


                ////FileStream fs = new FileStream(fileName, FileMode.Open);
                //var stream = new MemoryStream(Encoding.UTF8.GetBytes(responseResult));
                //XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(stream, new XmlDictionaryReaderQuotas());
                //DataContractSerializer ser = new DataContractSerializer(typeof(Hotels));
                //Hotels hotel = (Hotels)ser.ReadObject(reader, true);

                return hotel;
            }

        }


        [XmlRoot(ElementName = "CtripSubHotelInfoRS",Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Hotels
        {
            
            public int retCode { get; set; }
            public string errorMsg { get; set; }
            public List<HotelInfo> HotelInfos { get; set; }
        }

           
            public class HotelInfo
            {
          
                public string masterHotelId { get; set; }
                public string subHotelId { get; set; }
                public string hotelName { get; set; }
                public string address { get; set; }
                public string provinceName { get; set; }
                public string cityName { get; set; }
                public string hotelBelongto { get; set; }
            }


            //private static byte[] HashtableToSoap12(Hashtable ht, String XmlNs, String MethodName)
            //{
            //    XmlDocument doc = new XmlDocument();
            //    doc.LoadXml("<soap12:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap12=\"http://www.w3.org/2003/05/soap-envelope\"></soap12:Envelope>");
            //    XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            //    doc.InsertBefore(decl, doc.DocumentElement);
            //    XmlElement soapBody = doc.CreateElement("soap12", "Body", "http://www.w3.org/2003/05/soap-envelope");

            //    XmlElement soapMethod = doc.CreateElement(MethodName);
            //    soapMethod.SetAttribute("xmlns", XmlNs);
            //    foreach (string k in ht.Keys)
            //    {

            //        XmlElement soapPar = doc.CreateElement(k);
            //        soapPar.InnerXml = ObjectToSoapXml(ht[k]);
            //        soapMethod.AppendChild(soapPar);
            //    }
            //    soapBody.AppendChild(soapMethod);
            //    doc.DocumentElement.AppendChild(soapBody);
            //    return Encoding.UTF8.GetBytes(doc.OuterXml);
            //}


            //private static string ObjectToSoapXml(object o)
            //{
            //    XmlSerializer mySerializer = new XmlSerializer(o.GetType());
            //    MemoryStream ms = new MemoryStream();
            //    mySerializer.Serialize(ms, o);
            //    XmlDocument doc = new XmlDocument();
            //    doc.LoadXml(Encoding.UTF8.GetString(ms.ToArray()));
            //    if (doc.DocumentElement != null)
            //    {
            //        return doc.DocumentElement.InnerXml;
            //    }
            //    else
            //    {
            //        return o.ToString();
            //    }
            //}

        }
    }
