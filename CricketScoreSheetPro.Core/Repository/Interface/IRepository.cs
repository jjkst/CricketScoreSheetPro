﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Repository.Interface
{
    public interface IRepository<T>
    {
        T Create(T obj);
        T CreateWithParentId(string id, T obj);

        bool Update(string id, T val);

        IList<T> GetList();
        T GetItem(string uid);

        void Delete(string id);
    }
}
