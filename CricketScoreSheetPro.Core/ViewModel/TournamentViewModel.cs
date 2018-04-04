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
            Tournament = _tournamentService.GetTournament(tournamentId) ?? throw new ArgumentNullException($"Tournament Id is not exist");
        }

        public Tournament Tournament { get; private set; }

        public string ProvideAccess(AccessType accessType)
        {
            return $"{Tournament.Id} {accessType}";
        }

        public bool UpdateTournament()
        {
            var updated = _tournamentService.UpdateTournament(Tournament);
            return updated;
        }
    }
}
