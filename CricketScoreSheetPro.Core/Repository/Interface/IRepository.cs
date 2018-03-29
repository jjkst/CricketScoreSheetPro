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

        bool Update(string id, T val);

        IList<T> GetList();

        IList<T> GetListByProperty(string propertyName, string propertyValue);

        T GetItem(string id);

        void Delete(string id);
    }
}
