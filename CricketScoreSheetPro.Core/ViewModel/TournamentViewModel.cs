using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Service.Interface;
using System;

namespace CricketScoreSheetPro.Core.ViewModel
{
    public class TournamentViewModel
    {
        private readonly ITournamentService _tournamentService;

        public TournamentViewModel(ITournamentService tournamentService, string tournamentId)
        {
            _tournamentService = tournamentService ?? throw new ArgumentNullException($"TournamentService is null, cannot get tournaments.");
            Tournament = _tournamentService.GetTournamentDetail(tournamentId) ?? throw new ArgumentNullException($"Tournament Id is not exist");
        }

        public TournamentDetail Tournament { get; private set; }

        public string ProvideAccess(AccessType accessType)
        {
            return $"{Tournament.Id} {accessType}";
        }

        public bool UpdateTournament(TournamentDetail tournament)
        {
            Tournament = tournament;
            var updated = _tournamentService.UpdateTournament(tournament);
            return updated;
        }
    }
}
