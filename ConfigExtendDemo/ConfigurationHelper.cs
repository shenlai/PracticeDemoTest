
using ConfigExtendDemo.Section_CDATA;
using ConfigExtendDemo.Section_Collection;
using ConfigExtendDemo.Section_Element;
using ConfigExtendDemo.SectionExtendTest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigExtendDemo
{
    public class ConfigurationHelper
    {
        //section(property)
        public static readonly SectionExtendSection configSectionAtt = ConfigurationManager.GetSection("SectionExtend") as SectionExtendSection;

        //section element(property)
        public static readonly SectionExtend2 configSectionEle = ConfigurationManager.GetSection("SectionExtend2") as SectionExtend2;

        //section collection
        public static readonly SectionExtend3 configSectionCol = ConfigurationManager.GetSection("SectionExtend3") as SectionExtend3;

        public static readonly MySection configSectionCDATA = ConfigurationManager.GetSection("MySection333") as MySection;
    }
}
