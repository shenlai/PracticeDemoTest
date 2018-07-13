using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JsonHandle
{

    public class TestModel
    {
        [JsonIgnore]
        public string attstring { get; set; }
        public int attint { get; set; }
        public double attdouble { get; set; }

        public DateTime atttime { get; set; }
    }
}
