using Couchbase.Lite.Query;
using System;
using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Service.Interface
{
    public interface IDataSeedService<T>
    {
        string Create(T val);

        string Create(T val, params KeyValuePair<string, string>[] pairs);

        bool Update(string id, T val);

        IList<T> GetList();

        IList<T> GetFilteredList(IExpression filter);

        T GetItem(string id);

        void Delete(string id);
    }
}
