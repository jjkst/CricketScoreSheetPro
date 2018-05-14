using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CricketScoreSheetPro.Core.ViewModel
{
    public class TeamListViewModel 
    {
        private readonly ITeamService _teamService;
        private readonly IAccessService _accessService;

        public TeamListViewModel(ITeamService teamService, IAccessService accessService)
        {
            _teamService = teamService ?? throw new ArgumentNullException($"TeamService is null, cannot get teams.");
            _accessService = accessService ?? throw new ArgumentNullException($"AccessService is null, cannot get tournaments.");
        }

        public List<Team> Teams => _teamService.GetTeamList().ToList();

        public List<Team> ImportedTeams(ITournamentService tournamentService)
        {
            var access = _accessService.GetAccessList().FirstOrDefault(a => a.DocumentType == nameof(Tournament));
            var importedTeams = new List<Team>();
            foreach (var t in access.Documents)
            {
                var importedtournament = tournamentService.GetTournament(t.Id);
                importedTeams.AddRange(importedtournament.Teams);
            }
            return importedTeams;
        }

        public string AddTeam(string teamName)
        {
            var newteam = _teamService.AddTeam(new Team
            {
                Name = teamName,
                AddDate = DateTime.Today
            });
            return newteam;
        }

        public void DeleteTeam(string id)
        {
            _teamService.DeleteTeam(id);
        }

    }
}
