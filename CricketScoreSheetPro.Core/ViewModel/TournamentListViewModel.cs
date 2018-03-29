using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CricketScoreSheetPro.Core.ViewModel
{
    public class TournamentListViewModel
    {
        private readonly ITournamentService _tournamentService;

        public TournamentListViewModel(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService ?? throw new ArgumentNullException($"TournamentService is null, cannot get tournaments.");
        }

        public List<UserTournament> Tournaments => _tournamentService.GetUserTournaments().ToList();

        public UserTournament AddTournament(string tournamentName)
        {
            var newtournament = _tournamentService.AddTournament(tournamentName);
            return newtournament;
        }

        public void DeleteTournament(string id)
        {
            _tournamentService.DeleteTournament(id);
        }

        public UserTournament ImportTournament(string id_accesstype, string uuid)
        {
            var val = id_accesstype.Split(' ');
            var importedtournament = _tournamentService.ImportTournament(val[0], (AccessType) Enum.Parse(typeof(AccessType), val[1]));
            return importedtournament;
        }
    }
}
