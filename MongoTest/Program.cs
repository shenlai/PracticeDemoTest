using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
//using MongoDB.Driver.Linq;

namespace MongoTest
{
    //http://mongodb.github.io/mongo-csharp-driver/2.2/getting_started/quick_tour/
    public class Program
    {
        static void Main(string[] args)
        {
            string mongostr = "mongodb://127.0.0.1:27017";
            string dataBase = "test";

            var client = new MongoClient(mongostr);
            var db = client.GetDatabase(dataBase);
            //查询
            var personList1 = db.GetCollection<Person>("person");
            var personList = db.GetCollection<Person>("person").AsQueryable().Where(x => x.name == "tom").ToList();

            //Insert
            var perCollection = db.GetCollection<Person>("person");
            var person = new Person() { name = "shenlai", age = 20 };
            //perCollection.InsertOne(person);
            var id = person.Id;
            //InsertMany
            //List<Person> perList = new List<Person>() { new Person { name = "shen1", age = 12 }, new Person { name = "shen2", age = 13 } };
            //perCollection.InsertMany(perList);


            //var query = Query<Person>.EQ(p => p.name, "shenlai");
            //var shen = perCollection.Find(query);
            //查询 filter
            var filter = Builders<Person>.Filter.Eq(x => x.name, "shenlai");
            var shen = perCollection.Find(filter).FirstOrDefault();

            //排序
            var sort = Builders<Person>.Sort.Descending("age");
            var list = perCollection.Find(new BsonDocument()).Sort(sort).ToList();

            //更新

            //var upFilter = Builders<Person>.Filter.Eq(x => x.age, 31);
            //var update = Builders<Person>.Update.Set("name", "shen update");
            //var update1 = Builders<Person>.Update.Set(x=>x.name, "shen update");

            //var updateRes = perCollection.UpdateOne(upFilter, update1);


            var upmanyFilter = Builders<Person>.Filter.Eq(x => x.age, 2);
            var updateMany = Builders<Person>.Update.Set("name", "shenMany");
            var updateMany1 = Builders<Person>.Update.Set(x => x.name, "shen update");

            var updateManyRes = perCollection.UpdateMany(upmanyFilter, updateMany);

















            Console.ReadLine();

        }

        
    }

    public class Person
    {
        public ObjectId Id { get; set; }
        public string name { get; set; }

        public int age { get; set; }
    }
}
