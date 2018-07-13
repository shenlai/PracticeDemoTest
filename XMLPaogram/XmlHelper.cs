using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace XMLPaogram
{
    public class XmlHelper
    {
        #region  序列化读取XML

        /// <summary>
        /// 读取xml文件，反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static T XmlDeserializeFromXml<T>(string path, Encoding encoding)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");
            if (encoding == null)
                throw new ArgumentNullException("encoding");
            string xmlContent = File.ReadAllText(path);
            return XmlDeserialize<T>(xmlContent, encoding);

        }

        /// <summary>
        /// 将xml字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlContent"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static T XmlDeserialize<T>(string xmlContent, Encoding encoding)
        {
            if (string.IsNullOrEmpty(xmlContent))
                throw new ArgumentNullException("xmlContent");
            if (encoding == null)
                throw new ArgumentNullException("encoding");
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(encoding.GetBytes(xmlContent)))
            {
                using (StreamReader sr = new StreamReader(ms, encoding))
                {
                    return (T)serializer.Deserialize(sr);
                }
            }

        }

        #endregion
        public static XmlDocument GetXmlDoc(string fileName)
        {
            var xmldoc = new XmlDocument();
            xmldoc.Load(fileName);
            return xmldoc;
        }


        public static XmlNodeList GetNodeListByNodeName(XmlDocument xmldoc, string nodeName)
        {
            return xmldoc.SelectNodes(nodeName);
        }

        /// <summary>
        /// 根据Nodes,反射出T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static T GetTFromXmlNodeList<T>(XmlNodeList nodes) where T : new()
        {
            Type t = typeof(T);
            T model = new T();
            T m = new T();
            foreach (var typeInfo in t.GetProperties().Where(x => x.CanRead == true && x.CanWrite == true))
            {
                foreach (XmlElement e in nodes)
                {
                    if (typeInfo.Name == e.Name)
                    {
                        string value = e.InnerText;
                        typeInfo.SetValue(model, value);
                        break;
                    }

                }
                //XmlElement继承自 XmlNode
                foreach (XmlNode n in nodes)
                {
                    if (typeInfo.Name == n.Name)
                    {
                        string value = n.InnerText;
                        typeInfo.SetValue(m, value);
                        break;
                    }
                }
            }
            return model;
        }

        /// <summary>
        /// 节点读取（Element)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nodes">nodelist《AppSetting》</param>
        /// <param name="nodeName">节点key</param>
        /// <param name="key">节点value</param>
        /// <returns></returns>
        public static T GetTFromXmlNodeList<T>(XmlNodeList nodes, string nodeKey, string value) where T : new()
        {
            T t = new T();
            if (nodes != null)
            {
                XmlNodeList nodelist = nodes[0].ChildNodes;//取第一个AppSeting
                foreach (XmlNode node in nodelist)
                {
                    if (node.SelectSingleNode(nodeKey).InnerText.ToString() == value)
                    {
                        t = GetTFromXmlNodeList<T>(node.ChildNodes);
                        break;
                    }
                }
            }
            return t;
        }
    }
}
