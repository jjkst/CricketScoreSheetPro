using CricketScoreSheetPro.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CricketScoreSheetPro.Core.Service.Interface
{
    public interface IAccessService
    {
        string AddAccess(Access access);
        void DeleteAccess(string id);
        IList<Access> GetAccessList();
        Access GetAccess(string accessId);
        bool UpdateAccess(Access access);
    }
}
