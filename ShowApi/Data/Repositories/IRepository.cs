using ShowApi.Data.Entities;
using System.Collections.Generic;

namespace ShowApi.Data.Repositories
{
    public interface IRepository<T>
    {
        IList<T> GetAll();
        T GetById(string id);
        void DataFeed(T ent);
        IList<T> GetByFilter(object filter);
        T Save(T payload);
        public string Table { get; set; }
        long Delete(string id);
    }
}
