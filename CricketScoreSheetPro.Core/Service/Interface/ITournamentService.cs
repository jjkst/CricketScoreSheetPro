using CricketScoreSheetPro.Core.Model;
using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Service.Interface
{
    public interface ITournamentService
    {
        IList<Tournament> GetTournaments();
        TournamentDetail AddTournament(string tournamentName, string uuid);
        void DeleteTournament(string id);
        Tournament ImportTournament(string id, AccessType accessType, string uuid);
        TournamentDetail GetTournamentDetail(string tournamentId);
    }
}
