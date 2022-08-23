using ShowApi.Data.Entities;
using System.Collections.Generic;

namespace ShowApi.Data.Repositories
{
    public interface IRepository<T>
    {
        IList<T> GetAll(string table);
        T GetById(string id, string table);
        void DataFeed(T ent, string table);
        IList<T> GetByFilter(object filter);
        T Save(T payload, string table);
    }
}
