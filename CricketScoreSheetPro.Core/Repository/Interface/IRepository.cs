using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Repository.Interface
{
    public interface IRepository<T>
    {
        string Create(T val);

        bool Update(string id, T val);

        IList<T> GetList();

        T GetItem(string id);

        void Delete(string id);
    }
}
