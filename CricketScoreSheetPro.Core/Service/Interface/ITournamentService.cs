using CricketScoreSheetPro.Core.Model;
using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Service.Interface
{
    public interface ITournamentService
    {
        IList<Tournament> GetTournaments();
        TournamentDetail AddTournament(string tournamentName);
        void DeleteTournament(string id);
        Tournament ImportTournament(string id, AccessType accessType);
        TournamentDetail GetTournamentDetail(string tournamentId);
    }
}
