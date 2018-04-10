using CricketScoreSheetPro.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CricketScoreSheetPro.Core.Service.Interface
{
    public interface IUmpireService
    {
        Umpire AddUmpire(string Umpire);
        void DeleteUmpire(string id);
        IList<Umpire> GetUmpires();
        Umpire GetUmpire(string Id);
        bool UpdateUmpire(Umpire Umpire);
    }
}
