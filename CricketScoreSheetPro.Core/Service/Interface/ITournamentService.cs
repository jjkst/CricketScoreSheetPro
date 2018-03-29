using CricketScoreSheetPro.Core.Model;
using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Service.Interface
{
    public interface ITournamentService
    {        
        UserTournament AddTournament(string tournamentName);        
        UserTournament ImportTournament(string id, AccessType accessType);
        void DeleteTournament(string id);
        IList<UserTournament> GetUserTournaments();
        Tournament GetTournament(string tournamentId);
        bool UpdateTournament(Tournament tournamentdetail);
    }
}
