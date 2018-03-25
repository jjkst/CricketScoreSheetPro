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
        private readonly IRepository<TournamentDetail> _tournamentdetailRepository;

        public TournamentService(IRepository<Tournament> tournamentRepository, IRepository<TournamentDetail> tournamentdetailRepository)
        {
            _tournamentRepository = tournamentRepository ?? throw new ArgumentNullException($"TournamentRepository is null");
            _tournamentdetailRepository = tournamentdetailRepository ?? throw new ArgumentNullException($"TournamentDetailRepository is null");
        }

        public Tournament AddTournament(string tournamentName)
        {
            var newtournament = new Tournament
            {
                Id = "$replacewith_id$",
                Name = tournamentName,
                Status = "Open",
                AddDate = DateTime.Today                
            };
            var tournamentAdded = _tournamentRepository.Create(newtournament);
            var newtournamentdetail = new TournamentDetail
            {
                Name = newtournament.Name,
                Status = newtournament.Status,
                StartDate = newtournament.AddDate
            };
            var tournamentdetailAdded = _tournamentdetailRepository.CreateWithParentId(tournamentAdded.Id, newtournamentdetail);
            return tournamentAdded;
        }

        public IList<Tournament> GetTournaments()
        {
            var result = _tournamentRepository.GetList();
            return result;
        }
    }
}
