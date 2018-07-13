using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.IO;
using Newtonsoft.Json;

namespace LinqtosqlDemo
{
    class Program
    {
        #region
        //http://www.cnblogs.com/DebugLZQ/archive/2012/11/15/2772203.html
        //https://msdn.microsoft.com/en-us/library/system.io.file.appendtext.aspx
        #endregion
        static void Main(string[] args)
        {

            //连接符
            //string constring = @"Data Source=.\SQLEXPRESS;AttachDbFilename=E:\Visual Studio 2010\LINQ_to_SQL\LINQ_To_SQL自定义数据库和实体类\Database1.mdf;Integrated Security=True;User Instance=True";

            string constring = @"Data Source=.;Initial Catalog=Performance;Persist Security Info=True;User ID=sa;Password=123456";
            DataContext db = new DataContext(constring);

            string sql = @"select top 10 * from Customers";
            var list = db.ExecuteQuery<Customers>(sql).ToList();

            WriteDate(list);

            Console.ReadLine();


        
        }

        public static void WriteDate(List<Customers> data)
        {

            FileStream fs = new FileStream(@"D:\WorkSpace\PracticeDemoTest\LinqtosqlDemo\data.txt", FileMode.Append, FileAccess.Write, FileShare.Write, 100, true);
            byte[] writebytes = new byte[1000000];
            //这里要注意，如果写入的字符串很小，则.Net会使用辅助线程写，因为这样比较快
            string content = JsonConvert.SerializeObject(data);
            writebytes = Encoding.UTF8.GetBytes(content);
            
            fs.BeginWrite(writebytes, 0, writebytes.Length, null, fs);

        }
    }
}
