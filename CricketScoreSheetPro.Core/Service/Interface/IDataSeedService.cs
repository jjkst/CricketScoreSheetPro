using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Service.Interface
{
    public interface IDataSeedService<T>
    {
        string Create(T val);

        bool Update(string id, T val);

        IList<T> GetList();

        T GetItem(string id);

        void Delete(string id);
    }
}
