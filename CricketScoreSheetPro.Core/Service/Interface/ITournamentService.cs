using CricketScoreSheetPro.Core.Model;
using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Service.Interface
{
    public interface ITournamentService
    {        
        TournamentDetail AddTournament(string tournamentName);        
        Tournament ImportTournament(string id, AccessType accessType);
        void DeleteTournament(string id);
        IList<Tournament> GetTournaments();
        TournamentDetail GetTournamentDetail(string tournamentId);
        bool UpdateTournament(TournamentDetail tournamentdetail);
    }
}
