using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Repository.Interface;
using CricketScoreSheetPro.Core.Service.Interface;
using System;
using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Service.Implementation
{
    public class TournamentService : ITournamentService
    {
        private readonly IRepository<Tournament> _tournamentRepository;

        public TournamentService(IRepository<Tournament> tournamentRepository)
        {
            _tournamentRepository = tournamentRepository ?? throw new ArgumentNullException($"tournamentRepository is null");
        }

        public string AddTournament(Tournament newtournament)
        {
            if (newtournament == null) throw new ArgumentNullException($"newtournament is null");
            var tournamentdAdded = _tournamentRepository.Create(newtournament);
            return tournamentdAdded;
        }

        public void DeleteTournament(string tournamentid)
        {
            if (string.IsNullOrEmpty(tournamentid)) throw new ArgumentException($"Tournament ID is null");
            _tournamentRepository.Delete(tournamentid);
        }

        public IList<Tournament> GetTournamentList()
        {
            var result = _tournamentRepository.GetList();
            return result;
        }

        public Tournament GetTournament(string tournamentId)
        {
            if (string.IsNullOrEmpty(tournamentId)) throw new ArgumentException($"Tournament ID is null");
            var tournament = _tournamentRepository.GetItem(tournamentId);
            if (tournament == null) throw new ArgumentException($"Document does not exist.");
            return tournament;
        }

        public bool UpdateTournament(Tournament tournament)
        {
            if (tournament == null) throw new ArgumentException($"Tournament is null");
            return _tournamentRepository.Update(tournament.Id, tournament);
        }
    }
}
