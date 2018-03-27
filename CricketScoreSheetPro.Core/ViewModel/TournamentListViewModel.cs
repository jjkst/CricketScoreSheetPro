using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.ViewModel
{
    public class TournamentListViewModel
    {
        private readonly ITournamentService _tournamentService;

        public TournamentListViewModel(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService ?? throw new ArgumentNullException($"TournamentService is null, cannot get tournaments.");
        }

        public List<Tournament> Tournaments => _tournamentService.GetTournaments().ToList();


        public TournamentDetail AddTournament(string tournamentName, string uuid)
        {
            var newtournament = _tournamentService.AddTournament(tournamentName, uuid);
            return newtournament;
        }

        public void DeleteTournament(string id)
        {
            _tournamentService.DeleteTournament(id);
        }
    }
}
