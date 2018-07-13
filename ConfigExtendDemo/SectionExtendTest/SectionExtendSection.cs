using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ConfigExtendDemo.SectionExtendTest
{
    public class SectionExtendSection: ConfigurationSection
    {
        [ConfigurationProperty("username", IsRequired = true)] //调用 ConfigurationProperty构造函数
        public string Username
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
