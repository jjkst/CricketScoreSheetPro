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

        public TournamentDetail AddTournament(string tournamentName, string UUID)
        {
            var newtournamentproperties = new Dictionary<string, object>
            {
                { "uuid", UUID},
                { "type", nameof(Tournament)},
                { "value", new Tournament
                            {
                                Name = tournamentName,
                                Status = "Open",
                                AccessType = AccessType.Moderator,
                                Owner = UUID,
                                AddDate = DateTime.Today
                            }}
            };            
            var tournamentAdded = _tournamentRepository.Create(newtournamentproperties);

            var newtournamentdetailproperties = new Dictionary<string, object>
            {
                { "parent_id", tournamentAdded.Id},
                { "type", nameof(TournamentDetail)},
                { "value", new TournamentDetail
                            {
                                Id = tournamentAdded.Id,
                                Name = tournamentAdded.Name,
                                Status = tournamentAdded.Status,
                                StartDate = tournamentAdded.AddDate
                            }}
            };            
            var tournamentdetailAdded = _tournamentdetailRepository.Create(newtournamentdetailproperties);
            return tournamentdetailAdded;
        }

        public void DeleteTournament(string id)
        {
            _tournamentRepository.Delete(id);
        }

        public IList<Tournament> GetTournaments()
        {
            var result = _tournamentRepository.GetList();
            return result;
        }


    }
}
