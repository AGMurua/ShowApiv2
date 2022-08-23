using MongoDB.Bson;
using MongoDB.Driver;
using ShowApi.Data.Entities;
using System.Collections.Generic;

namespace ShowApi.Data.Repositories
{
    public class BaseRepository<T>: IRepository<T>
    {
        private string _database;
        public IMongoClient Client;
        public IMongoDatabase dataBase;
        public IMongoCollection<T> table;

        public BaseRepository()
        {
            Client = new MongoClient("mongodb+srv://admin:admin123@showapi.1o9ee1d.mongodb.net/?retryWrites=true&w=majority"); 
            dataBase = Client.GetDatabase("ShowApi");
        }

        public IList<T> GetAll(string table)
        {
            var feed = dataBase.GetCollection<T>(table);
            FilterDefinition<T> filter = Builders<T>.Filter.Empty;
            return feed.Find(filter).ToList();
        }

        public T GetById(string id, string table)
        {
            var feed = dataBase.GetCollection<T>(table);
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            var result = feed.Find(filter).FirstOrDefault();
            return result;
        }

        public void DataFeed(T ent, string table)
        {
            var feed = dataBase.GetCollection<T>(table);
            feed.InsertOne(ent);
        }

        public IList<T> GetByFilter(T filter)
        {
            throw new System.NotImplementedException();
        }

        public T Save(T payload, string table)
        {
            var feed = dataBase.GetCollection<T>(table);
            feed.InsertOne(payload);
            return payload;
        }

        public IList<T> GetByFilter(object filter)
        {
            throw new System.NotImplementedException();
        }
    }
}
