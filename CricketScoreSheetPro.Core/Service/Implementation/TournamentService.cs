using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Repository.Interface;
using CricketScoreSheetPro.Core.Service.Interface;
using System;
using System.Collections.Generic;

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
            if (string.IsNullOrEmpty(tournamentName)) throw new ArgumentException($"Tournament name is empty");
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
            if(tournamentdAdded == null) throw new ArgumentException($"Tournament add is not successful.");
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
            if (string.IsNullOrEmpty(tournamentid)) throw new ArgumentException($"Tournament ID is null");
            var existingtournament = _tournamentRepository.GetItem(tournamentid);
            if (existingtournament == null) throw new ArgumentException($"Not able to find tournamant.");
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
            if (string.IsNullOrEmpty(usertournamentid)) throw new ArgumentException($"UserTournament ID is null");
            var usertournament = _usertournamentRepository.GetItem(usertournamentid);
            if (usertournament == null) throw new ArgumentException($"Not able to find user tournamant.");
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
            if (string.IsNullOrEmpty(tournamentId)) throw new ArgumentException($"Tournament ID is null");
            var tournament = _tournamentRepository.GetItem(tournamentId);
            return tournament;
        }

        public bool UpdateTournament(Tournament tournament)
        {
            if (tournament == null) throw new ArgumentException($"Tournament is null");

            //update tournament
            var updatedtournament = _tournamentRepository.Update(tournament.Id, tournament);
            if (!updatedtournament) return false;

            // update usertournament
            var allaffectedusertournaments = _usertournamentRepository.GetListByProperty("tournament", tournament.Id);
            if(allaffectedusertournaments == null) throw new ArgumentException($"Not able to find tournament id in users list");
            if (allaffectedusertournaments[0].Name == tournament.Name ||
                allaffectedusertournaments[0].Status == tournament.Status)
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
