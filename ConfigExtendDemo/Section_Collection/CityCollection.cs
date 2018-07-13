using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace ConfigExtendDemo.Section_Collection
{

    //[ConfigurationCollection(typeof(CityElement))]
    [ConfigurationCollection(typeof(CityElement), AddItemName = "City")]  // AddItemName默认值 add
    public class CityCollection : ConfigurationElementCollection
    {
        #region
        // 基本上，所有的方法都只要简单地调用基类的实现就可以了。

        //public CityCollection()
        //    : base(StringComparer.OrdinalIgnoreCase)    // 忽略大小写
        //{
        //}

        //// 其实关键就是这个索引器。但它也是调用基类的实现，只是做下类型转就行了。
        //new public CityElement this[string name]
        //{
        //    get
        //    {
        //        return (CityElement)base.BaseGet(name);
        //    }
        //}
        #endregion

        #region 父类中抽象方法必须要实现。
        protected override ConfigurationElement CreateNewElement()
        {
            return new CityElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as CityElement).CityName;
            //return (element as CityElement).Key;
        }

        #endregion

        #region
        // 说明：如果不需要在代码中修改集合，可以不实现Add, Clear, Remove
        //public void Add(CityElement element)
        //{
        //    this.BaseAdd(element);
        //}
        //public void Clear()
        //{
        //    base.BaseClear();
        //}
        //public void Remove(string name)
        //{
        //    base.BaseRemove(name);
        //}
        #endregion
    }
}
