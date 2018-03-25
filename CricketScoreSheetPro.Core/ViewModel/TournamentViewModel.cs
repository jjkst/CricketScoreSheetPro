using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.ViewModel
{
    public class TournamentViewModel
    {
        private readonly ITournamentService _tournamentService;

        public TournamentViewModel(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService ?? throw new ArgumentNullException($"TournamentService is null, cannot get tournaments.");
        }

        public List<Tournament> Tournaments => _tournamentService.GetTournaments().ToList();

        public Tournament AddTournament(string tournamentName)
        {
            var newtournament = _tournamentService.AddTournament(tournamentName);
            return newtournament;
        }
    }
}
