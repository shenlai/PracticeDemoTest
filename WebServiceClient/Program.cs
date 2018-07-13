using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using static WebServiceClient.HttpWebRequestClient;

namespace WebServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {

            //方式一：途家网采用  返回null
            //var client = new WebServiceClient();
            //var res = client.GetCtripSubHotelInfo();




            //方式二 ：每次调用都生成动态DLL C:\Users\laishen\AppData\Local\Temp
            /*
            var param = new GetCtripSubHotelInfo() { masterHotelId = "66" };
            XmlSerializer serializer = new XmlSerializer(typeof(GetCtripSubHotelInfo));

            string strs = string.Empty;

            using (MemoryStream stream = new MemoryStream())
            {
                //XmlWriter writer = XmlWriter.Create(stream, new XmlWriterSettings() { OmitXmlDeclaration = false });
                XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);
                serializer.Serialize((XmlWriter)writer, param);
                writer.Close();
                strs = Encoding.UTF8.GetString(stream.ToArray());
                stream.Close();
            }

            string str = "http://124.127.242.67/automappingwebapi/automappingservices.asmx?wsdl";
            var clents = new WebServiceAgent(str);
            var ress= clents.Invoke("GetCtripSubHotelInfo", strs);
            
    */
            //方式三：Http方式
            //var res = HttpWebRequestClient.SoapV1_2Test();

            String url = "http://124.127.242.67/automappingwebapi/automappingservices.asmx";
            String soapAction = "http://htng.org/2014B/HTNG_ARIAndReservationPushService#GetCtripSubHotelInfo";

            var soapClient = new SoapClient(url, soapAction);
            var param = new GetCtripSubHotelInfo123() { masterHotelId = "121" };
            string responseResult = soapClient.GetResult(param);

            var hotel = XmlHelper.XmlDeserialize<Hotels>(responseResult, Encoding.UTF8);
            Console.ReadLine();
        }


    }
}
