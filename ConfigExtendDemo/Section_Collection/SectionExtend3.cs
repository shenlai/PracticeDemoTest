using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ConfigExtendDemo.Section_Collection
{
    public class SectionExtend3:ConfigurationSection
    {
        private static readonly ConfigurationProperty s_property = new ConfigurationProperty(string.Empty, typeof(CityCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);

        [ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
        //[ConfigurationCollection(typeof(CityElement), AddItemName = "City")]  // AddItemName默认值 add    此标签加在 CityCollection定义处也可以
        public CityCollection CityList
        {
            get { return base[s_property] as CityCollection; }
        }


        //other 实现
    }
}
