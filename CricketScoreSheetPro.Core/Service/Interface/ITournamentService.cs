using CricketScoreSheetPro.Core.Model;
using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Service.Interface
{
    public interface ITournamentService
    {
        string AddTournament(Tournament tournament);
        void DeleteTournament(string id);
        IList<Tournament> GetTournamentList();
        Tournament GetTournament(string tournamentId);
        bool UpdateTournament(Tournament tournament);
    }
}
