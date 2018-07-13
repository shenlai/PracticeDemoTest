using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigExtendDemo.Section_CDATA
{
    public class MySection:ConfigurationSection
    {
        [ConfigurationProperty("Command1",IsRequired=true)]
        public TextElement CommandOne 
        {
            get { return this["Command1"] as TextElement; }
        }

        [ConfigurationProperty("Command2", IsRequired = true)]
        public TextElement CommandTwo
        {
            get { return this["Command2"] as TextElement; }
        }
    }
}
