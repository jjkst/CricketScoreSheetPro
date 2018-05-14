using CricketScoreSheetPro.Core.Model;
using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Service.Interface
{
    public interface IMatchService
    {
        string AddMatch(Match match);
        void DeleteMatch(string id);
        IList<Match> GetMatchList();
        Match GetMatch(string matchId);
        bool UpdateMatch(Match match);
    }
}
