using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigExtendDemo.Section_CDATA
{
    //每个ConfigurationElement由我们来控制如何读写XML，也就是要重载方法SerializeElement，DeserializeElement
    public class TextElement:ConfigurationElement
    {

        protected override void DeserializeElement(System.Xml.XmlReader reader, bool serializeCollectionKey)
        {
            //base.DeserializeElement(reader, serializeCollectionKey);
            CommandText = reader.ReadElementContentAs(typeof(string), null) as string;
        }

        protected override bool SerializeElement(System.Xml.XmlWriter writer, bool serializeCollectionKey)
        {
            //return base.SerializeElement(writer, serializeCollectionKey);
            if (writer != null)
                writer.WriteCData(CommandText);
            return true;
        }



        [ConfigurationProperty("data", IsRequired = false)]
        public string CommandText
        {
            get { return this["data"].ToString(); }
            set { this["data"] = value; }
        }
    }
}
