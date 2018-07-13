using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ConfigExtendDemo.Section_Collection
{
    public class CityElement:ConfigurationElement
    {
        [ConfigurationProperty("cityname",IsRequired=true)]
        public string CityName
        {
            get { return this["cityname"].ToString(); }
        }

        [ConfigurationProperty("content",IsRequired=true)]
        public string Content
        {
            get { return this["content"].ToString(); }
        }
    }
}
