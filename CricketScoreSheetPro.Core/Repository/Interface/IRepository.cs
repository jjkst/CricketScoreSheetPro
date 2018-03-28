using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Repository.Interface
{
    public interface IRepository<T>
    {
        T Create(Dictionary<string, object> property);

        T ImportCreate(Dictionary<string, object> property);

        bool Update(string id, T val);

        IList<T> GetList();

        T GetItem(string uid);

        T GetChildItem(string uid);

        void Delete(string id);
    }
}
