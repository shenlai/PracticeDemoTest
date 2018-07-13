using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ConfigExtendDemo.Section_Element
{

    public class SectionElementUser:ConfigurationElement
    {
        [ConfigurationProperty("username",IsRequired=true)]
        public string UserName
        {
            get { return this["username"].ToString(); }
            set { this["username"] = value; }
        }

        [ConfigurationProperty("url", IsRequired = true)]
        public string Url
        {
            get { return this["url"].ToString(); }
            set { this["url"] = value; }
        }
    }
}
