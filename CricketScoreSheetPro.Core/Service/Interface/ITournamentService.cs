using CricketScoreSheetPro.Core.Model;
using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Service.Interface
{
    public interface ITournamentService
    {
        IList<Tournament> GetTournaments();
        Tournament AddTournament(string tournamentName);
    }
}
