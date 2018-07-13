using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace LinqtosqlDemo
{
    [Table(Name = "Customers")]
    public class Customers
    {
        [Column(Name = "CustomerID")]
        public int CustomerID { get; set; }

        [Column(Name = "CustomerNumber")]
        public string CustomerNumber { get; set; }

        [Column(Name = "CustomerName")]
        public string CustomerName { get; set; }

        [Column(Name = "CustomerCity")]
        public string CustomerCity { get; set; }
    }
}
