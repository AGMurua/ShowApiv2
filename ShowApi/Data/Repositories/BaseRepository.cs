using MongoDB.Bson;
using MongoDB.Driver;
using ShowApi.Data.Entities;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ShowApi.Data.Repositories
{
    public class BaseRepository<T>: IRepository<T>
    {
        internal IMongoClient Client;
        internal IMongoDatabase dataBase;
        public string Table { get; set; }
         

        public BaseRepository(MongoDBConnection connection)
        {
            Client = connection.Connect();
            dataBase = Client.GetDatabase("ShowApi");
        }

        public IList<T> GetAll()
        {
            var feed = dataBase.GetCollection<T>(Table);
            FilterDefinition<T> filter = Builders<T>.Filter.Empty;
            return feed.Find(filter).ToList();
        }

        public T GetById(string id)
        {
            var feed = dataBase.GetCollection<T>(Table);
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            var result = feed.Find(filter).FirstOrDefault();
            return result;
        }

        public void DataFeed(T ent)
        {
            var feed = dataBase.GetCollection<T>(Table);
            feed.InsertOne(ent);
        }

        public IList<T> GetByFilter(T filter)
        {
            throw new System.NotImplementedException();
        }

        public T Save(T payload)
        {
            var feed = dataBase.GetCollection<T>(Table);
            feed.InsertOne(payload);
            return payload;
        }

        public IList<T> GetByFilter(object filter)
        {
            throw new System.NotImplementedException();
        }

        public long Delete(string id)
        {
            var feed = dataBase.GetCollection<T>(Table);
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            var test = feed.DeleteOne(filter);
            return test.DeletedCount;
        }

        public ReplaceOneResult Update(T entity, string id)
        {
            var feed = dataBase.GetCollection<T>(Table);
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            var replaceResult = feed.ReplaceOne(filter, entity);
            return replaceResult;
        }
    }

    public class MongoDBConnection
    {
        public IMongoClient Connect()
        {
            return new MongoClient("mongodb+srv://admin:admin123@showapi.1o9ee1d.mongodb.net/?retryWrites=true&w=majority");
        }
    }

}
