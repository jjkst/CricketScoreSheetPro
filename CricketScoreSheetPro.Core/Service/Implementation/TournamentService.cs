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

        public TournamentDetail AddTournament(string tournamentName)
        {
            var uuid = _tournamentRepository.GetUUID();
            var newtournamentproperties = new Dictionary<string, object>
            {
                { "uuid", uuid},
                { "type", nameof(Tournament)},
                { "value", new Tournament
                            {
                                Name = tournamentName,
                                Status = "Open",
                                AccessType = AccessType.Moderator,
                                Owner = uuid,
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

        public Tournament ImportTournament(string id, AccessType accessType)
        {
            var uuid = _tournamentRepository.GetUUID();
            var existingtournament = _tournamentRepository.GetItem(id);
            var importtournamentproperties = new Dictionary<string, object>
            {
                { "uuid", uuid},
                { "type", nameof(Tournament)},
                { "value", new Tournament
                            {
                                Id = existingtournament.Id,
                                Name = existingtournament.Name,
                                Status = existingtournament.Status,
                                AccessType = accessType,
                                Owner = existingtournament.Owner,
                                AddDate = existingtournament.AddDate
                            }}
            };
            var importedtournament = _tournamentRepository.ImportCreate(importtournamentproperties);
            return importedtournament;
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

        public TournamentDetail GetTournamentDetail(string tournamentId)
        {
            var tournamentdetail = _tournamentdetailRepository.GetItem(tournamentId);
            return tournamentdetail;
        }
    }
}
