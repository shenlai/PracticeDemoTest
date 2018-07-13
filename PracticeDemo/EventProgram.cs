using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeDemo
{
    class EventProgram
    {
        #region 
        /*
         * 
         */
        #endregion

        //static void Main(string[] args)
        //{
        //    /*委托实现*/
        //    //Thermostat thermostat = new Thermostat();
        //    //Heater heater = new Heater(60);
        //    //Cooler cooler = new Cooler(80);
        //    //string temperature;
        //    //thermostat.OnTemperatureChange += heater.OnTemperatureChanged;
        //    //thermostat.OnTemperatureChange += cooler.OnTemperatureChanged;
            
        //    //Console.WriteLine("输入温度：");
        //    //temperature= Console.ReadLine();
        //    //thermostat.CurrentTemperature = int.Parse(temperature);//内部触发事件
        //    ////thermostat.OnTemperatureChange(100);//外部调用委托（触发事件)
        //    //Console.ReadLine();

        //    /*事件实现*/
        //    //Thermostat thermostat = new Thermostat();
        //    //Heater heater = new Heater(60);
        //    //Cooler cooler = new Cooler(80);
        //    //string temperature;
        //    //thermostat.OnTemperatureChange += heater.OnTemperatureChanged;
        //    //thermostat.OnTemperatureChange += cooler.OnTemperatureChanged;
        //    //Console.WriteLine("输入温度：");
        //    //temperature = Console.ReadLine();
        //    //thermostat.CurrentTemperature = int.Parse(temperature);//内部触发事件
        //    ////thermostat.OnTemperatureChange(100);//外部调用委托（触发事件)
        //    //Console.ReadLine();
        //}
    }

    public class Cooler
    {
        public Cooler(float temperature)
        {
            Temperature = temperature;
        }

        public float Temperature
        {
            get { return this._Temperature; }
            set { this._Temperature = value; }
        }

        private float _Temperature;


        public void OnTemperatureChanged(float newTemperature)
        {
            if (newTemperature > Temperature)
            {
                Console.WriteLine("Cooler:On");
            }
            else
            {
                Console.WriteLine("Cooler:Off");
            }
        }

    }

    public class Heater
    {
        public Heater(float temperature)
        {
            Temperature = temperature;
        }

        public float Temperature
        {
            get { return this._Temperature; }
            set { this._Temperature = value; }
        }

        private float _Temperature;


        public void OnTemperatureChanged(float newTemperature)
        {
            if (newTemperature < Temperature)
            {
                Console.WriteLine("Heater:On");
            }
            else
            {
                Console.WriteLine("Heater:Off");
            }
        }

    }


    public class Thermostat
    {
        #region
        /*委托实现*/
        //public delegate void TemperatureChangedHandler(float newTemperature);

        ////定义事件发布者
        //public TemperatureChangedHandler OnTemperatureChange
        //{
        //    get { return _OnTemperatureChange; }
        //    set { _OnTemperatureChange = value; }
        //}

        //private TemperatureChangedHandler _OnTemperatureChange;
        //public float CurrentTemperature
        //{
        //    get{return _CurrentTemperature;}
        //    set
        //    {
        //        if(value!=CurrentTemperature)
        //        {
        //            _CurrentTemperature = value;
        //            OnTemperatureChange(value);//委托调用
        //        }
        //    }
        //}

        //private float _CurrentTemperature;
        #endregion

        /*事件实现*/

        /*变更点：
         * 1. OnTemperatureChange 由属性变成一个public event 字段
         *    事件禁止使用赋值运算符 eventA = xxx
         * 2. 不允许在外部 使用thermostat.OnTemperatureChange(100)调用委托（阻止外部发布事件）
         */

        //定义委托数据类型
        public delegate void TemperatureChangeHandler(float newTemperature);

        //定义事件发布者
        public event TemperatureChangeHandler OnTemperatureChange = delegate { };

        public float CurrentTemperature
        {
            get { return _CurrentTemperature; }
            set
            {
                if (value != CurrentTemperature)
                {
                    _CurrentTemperature = value;
                    //OnTemperatureChange(value);//委托调用
                    if(OnTemperatureChange!=null)//委托null值检查
                    {
                        OnTemperatureChange(value);//只能在内部调用
                    }
                }
            }
        }

        private float _CurrentTemperature;

    }

}
