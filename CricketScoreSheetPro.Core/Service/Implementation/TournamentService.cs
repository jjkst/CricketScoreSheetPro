using CricketScoreSheetPro.Core.Helper;
using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Repository.Implementation;
using CricketScoreSheetPro.Core.Repository.Interface;
using CricketScoreSheetPro.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CricketScoreSheetPro.Core.Service.Implementation
{
    public class TournamentService : ITournamentService
    {
        private readonly IRepository<UserTournament> _usertournamentRepository;
        private readonly IRepository<Tournament> _tournamentRepository;

        public TournamentService(IRepository<UserTournament> usertournamentRepository, IRepository<Tournament> tournamentRepository)
        {
            _usertournamentRepository = usertournamentRepository ?? throw new ArgumentNullException($"usertournamentRepository is null");
            _tournamentRepository = tournamentRepository ?? throw new ArgumentNullException($"tournamentRepository is null");
        }

        public UserTournament AddTournament(string tournamentName)
        {
            var newtournamentproperties = new Dictionary<string, object>
            {
                { "type", nameof(Tournament)},
                { "value", new Tournament
                            {
                                Name = tournamentName,
                                Status = "Open",
                                StartDate = DateTime.Today
                            }}
            };
            var tournamentdAdded = _tournamentRepository.Create(newtournamentproperties);

            var newusertournamentproperties = new Dictionary<string, object>
            {
                { "tournament", tournamentdAdded.Id },
                { "type", nameof(UserTournament)},
                { "value", new UserTournament
                            {
                                TournamentId = tournamentdAdded.Id,
                                Name = tournamentdAdded.Name,
                                Status = tournamentdAdded.Status,
                                AccessType = AccessType.Moderator,
                                Owner = true,
                                AddDate = DateTime.Today
                            }}
            };            
            var usertournamentAdded = _usertournamentRepository.Create(newusertournamentproperties);

            return usertournamentAdded;
        }

        public UserTournament ImportTournament(string tournamentid, AccessType accessType)
        {
            var existingtournament = _tournamentRepository.GetItem(tournamentid);
            var importtournamentproperties = new Dictionary<string, object>
            {
                { "tournament", existingtournament.Id },
                { "type", nameof(UserTournament)},
                { "value", new UserTournament
                            {
                                TournamentId = existingtournament.Id,
                                Name = existingtournament.Name,
                                Status = existingtournament.Status,
                                AccessType = accessType,
                                AddDate = DateTime.Today
                            }}
            };
            var importedtournament = _usertournamentRepository.Create(importtournamentproperties);
            return importedtournament;
        }

        public void DeleteTournament(string usertournamentid)
        {
            var usertournament = _usertournamentRepository.GetItem(usertournamentid);
            _tournamentRepository.Delete(usertournament.TournamentId);
            _usertournamentRepository.Delete(usertournamentid);            
        }

        public IList<UserTournament> GetUserTournaments()
        {
            var result = _usertournamentRepository.GetList();
            return result;
        }

        public Tournament GetTournament(string tournamentId)
        {
            var tournament = _tournamentRepository.GetItem(tournamentId);
            return tournament;
        }

        public bool UpdateTournament(Tournament tournament)
        {
            //update tournament
            var updatedtournament = _tournamentRepository.Update(tournament.Id, tournament);

            // update usertournament
            var allaffectedusertournaments = _usertournamentRepository.GetListByProperty("tournament", tournament.Id);
            if (allaffectedusertournaments.Any() && (allaffectedusertournaments[0].Name == tournament.Name ||
                allaffectedusertournaments[0].Status == tournament.Status))
                return updatedtournament;

            bool updatedusertournament = true;
            foreach (var ut in allaffectedusertournaments)
            {
                ut.Name = tournament.Name;
                ut.Status = tournament.Status;
                updatedusertournament = updatedusertournament && _usertournamentRepository.Update(ut.Id, ut);
            }            
            
            return updatedtournament && updatedusertournament;
        }
    }
}
