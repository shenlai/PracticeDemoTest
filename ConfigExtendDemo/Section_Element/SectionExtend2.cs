using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ConfigExtendDemo.Section_Element
{
    public class SectionExtend2: ConfigurationSection
    {
        [ConfigurationProperty("User", IsRequired = true)] //调用 ConfigurationProperty构造函数
        public SectionElementUser User
        {
            get { return this["User"] as SectionElementUser; }
        }

        

    }
}
