using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using XMLPaogram.Model;

namespace XMLPaogram
{
    class Program
    {
        static void Main(string[] args)
        {
            /*两种类型的XML分别读取*/
            /*序列化与反序列化*/
            //string path = HostingEnvironment.MapPath("~/NodesData.Xml");
            //string path1 = Environment.CurrentDirectory + "NodesData.Xml";

            //string path = @"D:\Project\PracticeDemo\PracticeDemoTest\XMLPaogram\NodesData.xml";
            //var res = XmlHelper.XmlDeserializeFromXml<AppSetting>(path, System.Text.Encoding.UTF8);

            //string path2 = @"F:\Project\PracticeDemo\XMLPaogram\AttributeValueData.xml";
            //var res2 = XmlHelper.XmlDeserializeFromXml<ApplyInfoMap>(path2, System.Text.Encoding.UTF8);


            /*节点+反射读取XML*/

            /*
             * XmlDocument
             * XmlNodeList
             * XmlElement
             * XDocument
             * 
             */

            //var xmldoc = XmlHelper.GetXmlDoc(path);
            //XmlNodeList nodeList = XmlHelper.GetNodeListByNodeName(xmldoc, "AppSetting");
            //XmlNode node = xmldoc.SelectSingleNode("AppSetting");
            //var t = XmlHelper.GetTFromXmlNodeList<App>(nodeList, "AppName", "RZT_IOS");


            //path2

            /*XML读取*/
            //var xmldocAtt = XmlHelper.GetXmlDoc(path2);
            //var query = from item in XDocument.Load(path2).Element("ApplyInfoMap").Elements()
            //            select item;


            //XmlNodeList nodeListAtt = XmlHelper.GetNodeListByNodeName(xmldocAtt, "ApplyInfoMap");
            //ApplyInfoMap result = new ApplyInfoMap();
            //foreach(XmlNode no in nodeListAtt[0].SelectNodes("add"))
            //{
            //    ApplyInfoMapModelTest model = GetTFromXmlElement<ApplyInfoMapModelTest>(no);
            //    result.list.Add(model);
            //}

            /*
            var model = new ApplyInfoMapModelTest();
            model.AppKey = "AppKey";
            model.ApiKey = "ApiKey";
            model.ApiClass = "ApiKey";
            model.ApiList = "ApiList";
            model.RealClass = "Real";
            model.Desxxx = "描述";
            model.List = new List<string>() { "袁术1", "袁术2" };

    */



            /*

            var modelTest = new HotelAvailNotifRequest()
            {
                AvailStatusMessages = new AvailStatusMessages()
                {
                    HotelCode = "123",
                    list = new List<AvailStatusMessage>()
                     {
                          new AvailStatusMessage() {
                               RestrictionStatus = new RestrictionStatus()
                               {
                                   Restriction="Restriction",
                                   Status = "stat"
                               },
                                StatusApplicationControl = new StatusApplicationControl() {
                                     DestinationSystemCodes = new List<string>() { "Ctrip", "Elong"},
                                      End="121",
                                       InvTypeCode="324"
                                }

                          },
                          new AvailStatusMessage()
                          {
                               RestrictionStatus = new RestrictionStatus()
                               {
                                   Restriction="Restr",
                                   Status = "stat123123"
                               },
                                StatusApplicationControl = new StatusApplicationControl() {
                                     DestinationSystemCodes = new List<string>() { "Ctrip", "Elong"},
                                      End="2017-09-12",
                                       InvTypeCode="xxxx"
                                }

                          }

                     }
                }
            };



    */
            var mTest = new OTA_HotelAvailNotifRS()
            {
                //Success = ""
                Errors = new List<string>() { "错误" }
            };

            var mm = new OTA_HotelAvailRS()
            {
                Errors = new List<Error>() {
                      new Error() {
                           Code="BS0001",
                            Messages = "错误描述"
                      }
                 }
            };


            XmlSerializer serializer = new XmlSerializer(typeof(OTA_HotelAvailNotifRS));

            string str = string.Empty;

            using (MemoryStream stream = new MemoryStream())
            {
                XmlWriter writer = XmlWriter.Create(stream, new XmlWriterSettings()
                {
                    OmitXmlDeclaration = true //不生成声明头version
                });
                //XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);

                XmlSerializerNamespaces xns = new XmlSerializerNamespaces();
                xns.Add("", "http://www.opentravel.org/OTA/2003/05");
                serializer.Serialize((XmlWriter)writer, mTest, xns);
                writer.Close();
                str = Encoding.UTF8.GetString(stream.ToArray());
                stream.Close();
            }

            Console.ReadLine();
        }

        /// <summary>
        /// 属性读取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        public static T GetTFromXmlElement<T>(XmlNode node) where T : new()
        {
            T t = new T();
            Type typeInfo = typeof(T);

            foreach (var item in typeInfo.GetProperties().Where(x => x.CanRead == true && x.CanWrite == true))
            {
                foreach (XmlAttribute x in node.Attributes)
                {
                    if (item.Name == x.Name)
                    {
                        item.SetValue(t, x.Value);
                        break;
                    }
                }
            }
            return t;
        }

    }

    [XmlRoot(ElementName = "OTA_HotelAvailNotifRS", Namespace = "http://www.opentravel.org/OTA/2003/05")]
    public class OTA_HotelAvailNotifRS
    {
        //空值不序列化
        public string Success { get; set; }


        [XmlArrayItem("Error")]
        public List<string> Errors { get; set; }
    }



    /*
     * <OTA_HotelAvailRS xmlnsxmlnsxmlnsxmlnsxmlns=" http://www.opentravel.org/OTA/2003/05"> 
     *      <Errors> 
     *          <Error Code="BS0001">错误描述</Error>
     *      </Errors>
     *  </OTA_HotelAvailRS>
     */
    public class OTA_HotelAvailRS
    {
        //[XmlArrayItem("Error")]
        public List<Error> Errors { get; set; }
    }

    public class Error
    {
        [XmlAttribute]
        public string Code { get; set; }

        [XmlText]/**************************/
        public string Messages { get; set; }
    }



    #region 
    /// <summary>
    /// 推送房态
    /// </summary>
    [XmlRoot(ElementName = "GetCtripSubHotelInfo", Namespace = "http://www.opentravel.org/OTA/2003/05")]
    public class HotelAvailNotifRequest
    {
        //HotelCode  怎么序列化？？？？
        public AvailStatusMessages AvailStatusMessages { get; set; }
    }


    public class AvailStatusMessages
    {
        [XmlAttribute]
        public string HotelCode { get; set; }

        [XmlElement("AvailStatusMessage")]
        public List<AvailStatusMessage> list { get; set; }
    }

    public class AvailStatusMessage
    {
        public StatusApplicationControl StatusApplicationControl { get; set; }

        public RestrictionStatus RestrictionStatus { get; set; }
    }

    public class StatusApplicationControl
    {
        [XmlAttribute]
        public string Start { get; set; }

        [XmlAttribute]
        public string End { get; set; }

        [XmlAttribute]
        public string InvTypeCode { get; set; }
        [XmlAttribute]
        public string RatePlanCode { get; set; }

        [XmlAttribute]
        public string Mon { get; set; }

        [XmlAttribute]
        public string Tue { get; set; }

        [XmlAttribute]
        public string Weds { get; set; }

        [XmlAttribute]
        public string Thur { get; set; }

        [XmlAttribute]
        public string Fri { get; set; }

        [XmlAttribute]
        public string Sat { get; set; }

        [XmlAttribute]
        public string Sun { get; set; }


        [XmlArrayItem("DestinationSystemCode")]
        public List<string> DestinationSystemCodes { get; set; }
    }


    public class RestrictionStatus
    {
        [XmlAttribute]
        public string Restriction { get; set; }


        [XmlAttribute]
        public string Status { get; set; }
    }
    #endregion

}

