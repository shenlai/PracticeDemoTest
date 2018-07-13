using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace WebServiceClient
{
    [WebServiceBinding(Name = "AutoMappingServicesSoap", Namespace = "http://www.opentravel.org/OTA/2003/05")] //默认命名空间
    [DesignerCategory("code")]
    public class WebServiceClient : SoapHttpClientProtocol
    {
        public CtripSoapHeader CtripSoapHeaderValue { get; set; }
        public WebServiceClient()
        {
            CtripSoapHeaderValue = new CtripSoapHeader();
            base.Url = "http://124.127.242.67/automappingwebapi/automappingservices.asmx";
        }

        /*
         * [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/NTAccount",

        RequestNamespace = "http://tempuri.org/",

        ResponseNamespace = "http://tempuri.org/",

        Use = System.Web.Services.Description.SoapBindingUse.Literal,

        ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
         */
        //[System.Diagnostics.DebuggerStepThroughAttribute()]
        //[SoapHeader("CtripSoapHeaderValue"), 
        [SoapHeader("CtripSoapHeaderValue"),
            SoapDocumentMethodAttribute("http://htng.org/2014B/HTNG_ARIAndReservationPushService#GetCtripSubHotelInfo",
            RequestNamespace = "http://www.opentravel.org/OTA/2003/05",
            ResponseNamespace = "http://www.opentravel.org/OTA/2003/05", //http://schemas.xmlsoap.org/soap/envelope/
            Use = SoapBindingUse.Literal,
            ParameterStyle = SoapParameterStyle.Wrapped)]
        public string GetCtripSubHotelInfo() //
        {

            try
            {
                var param = new GetCtripSubHotelInfo123() { masterHotelId = "66" };
                XmlSerializer serializer = new XmlSerializer(typeof(GetCtripSubHotelInfo123));

                string str = string.Empty;

                using (MemoryStream stream = new MemoryStream())
                {
                    //Create our own namespaces for the output
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    //Add an empty namespace and empty value
                    ns.Add("", "");

                    //XmlWriter writer = XmlWriter.Create(stream, new XmlWriterSettings() { OmitXmlDeclaration = false });
                    XmlTextWriter writer = new XmlTextWriter(stream, Encoding.Default);
                    serializer.Serialize((XmlWriter)writer, param, ns);
                    writer.Close();
                    str = Encoding.Default.GetString(stream.ToArray());
                    stream.Close();
                }

                //var pp = "<?xml version=\"1.0\" encoding=\"utf-8\"?>< soap:Envelope xmlns:xsi = \"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd = \"http://www.w3.org/2001/XMLSchema\" xmlns:soap = \"http://schemas.xmlsoap.org/soap/envelope/\"><soap:Body><GetCtripSubHotelInfo xmlns =\"http://www.opentravel.org/OTA/2003/05/\"><masterHotelId>123</masterHotelId ></GetCtripSubHotelInfo></soap:Body></soap:Envelope>";

                

                //object[] result = base.Invoke("GetCtripSubHotelInfo", new object[] { str });//actionName ,参数
                object[] result = base.Invoke("GetCtripSubHotelInfo", new object[] { str });//actionName ,参数

                return ((string)(result[0]));

            }
            catch (Exception e)
            {
                return null;
            }



        }





    }



    public class CtripSoapHeader : SoapHeader
    {

    }
}
