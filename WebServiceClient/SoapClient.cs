using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace WebServiceClient
{

    /// <summary>
    /// SOAP辅助类
    /// </summary>
    public static class CtripSoapHelper
    {
        /// <summary>
        /// SOAP请求体格式
        /// </summary>
        private const string SOAP_ENVELOPE_FORMAT = @"<?xml version='1.0' encoding='utf-8'?>
                <soap:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/'>
                  <soap:Body>
                    {0}
                  </soap:Body>
                </soap:Envelope>";

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="soapAction">SOAP动作</param>
        /// <param name="soapParameters">参数集合</param>
        /// <returns>返回值</returns>
        public static string MakeSoapEnvelope(string xmlSoapBody)
        {

            return string.Format(SOAP_ENVELOPE_FORMAT, xmlSoapBody);
        }

        public static string SoapBodyXmlSerializer<T>(T request)where T:class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            string str = string.Empty;
            using (MemoryStream stream = new MemoryStream())
            {
                XmlWriter writer = XmlWriter.Create(stream, new XmlWriterSettings()
                {
                    OmitXmlDeclaration = true //不生成声明头version
                });
                XmlSerializerNamespaces xns = new XmlSerializerNamespaces();
                xns.Add("", "http://www.opentravel.org/OTA/2003/05");  //命名空间
                serializer.Serialize((XmlWriter)writer, request, xns);
                writer.Close();
                str = Encoding.UTF8.GetString(stream.ToArray());
                stream.Close();
                return str;
            }
        }
        
    }


    /// <summary>
    /// SOAP客户端
    /// </summary>
    public sealed class SoapClient
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="uriString">请求地址</param>
        /// <param name="soapAction">SOAP动作</param>
        public SoapClient(String uriString, String soapAction)
            : this(new Uri(uriString), soapAction) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="uri">请求地址</param>
        /// <param name="soapAction">SOAP动作</param>
        public SoapClient(Uri uri, String soapAction)
        {
            this.Uri = uri;
            this.SoapAction = soapAction;
            this.Credentials = CredentialCache.DefaultNetworkCredentials;
        }
        
        /// <summary>
        /// 身份凭证
        /// </summary>
        public ICredentials Credentials { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        /// SOAP动作
        /// </summary>
        public String SoapAction { get; set; }

        /// <summary>
        /// 获取响应
        /// </summary>
        /// <returns>响应</returns>
        public WebResponse GetResponse(string xmlSoapBody)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(this.Uri);
            webRequest.Headers.Add("SOAPAction", String.Format("\"{0}\"", this.SoapAction));
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            webRequest.Credentials = this.Credentials;

            // 写入请求SOAP信息
            using (var requestStream = webRequest.GetRequestStream())
            {
                using (var textWriter = new StreamWriter(requestStream))
                {
                    var envelope = CtripSoapHelper.MakeSoapEnvelope(xmlSoapBody);

                    if (!String.IsNullOrEmpty(envelope))
                        textWriter.Write(envelope);
                }
            }

            // 获取SOAP请求返回
            return webRequest.GetResponse();
        }

        /// <summary>
        /// 获取返回结果
        /// </summary>
        /// <returns>返回值</returns>
        public string GetResult<T>(T request) where T:class
        {
            var xmlRequest = CtripSoapHelper.SoapBodyXmlSerializer<T>(request);
            // 获取响应
            var webResponse = this.GetResponse(xmlRequest);
            var xmlReader = XmlTextReader.Create(webResponse.GetResponseStream());
            var xmlDocument = new XmlDocument();

            // 加载响应XML
            xmlDocument.Load(xmlReader);

            var nsmgr = new XmlNamespaceManager(xmlDocument.NameTable);
            nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");

            var bodyNode = xmlDocument.SelectSingleNode("soap:Envelope/soap:Body", nsmgr);

            return bodyNode.FirstChild.InnerXml;
        }
    }
}
