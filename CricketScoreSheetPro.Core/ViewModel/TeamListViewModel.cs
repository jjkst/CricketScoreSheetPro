using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CricketScoreSheetPro.Core.ViewModel
{
    public class TeamListViewModel 
    {
        private readonly IDataSeedService<Team> _teamService;
        private readonly IDataSeedService<Access> _accessService;

        public TeamListViewModel(IDataSeedService<Team> teamService, IDataSeedService<Access> accessService)
        {
            _teamService = teamService ?? throw new ArgumentNullException($"TeamService is null, cannot get teams.");
            _accessService = accessService ?? throw new ArgumentNullException($"AccessService is null, cannot get tournaments.");
        }

        public List<Team> Teams => _teamService.GetList().ToList();

        public List<Team> ImportedTeams(IDataSeedService<Tournament> tournamentService)
        {
            var access = _accessService.GetList().FirstOrDefault(a => a.DocumentType == nameof(Tournament));
            var importedTeams = new List<Team>();
            foreach (var t in access.Documents)
            {
                var importedtournament = tournamentService.GetItem(t.Id);
                importedTeams.AddRange(importedtournament.Teams);
            }
            return importedTeams;
        }

        public string AddTeam(string teamName)
        {
            var newteam = _teamService.Create(new Team
            {
                Name = teamName,
                AddDate = DateTime.Today
            });
            return newteam;
        }

        public void DeleteTeam(string id)
        {
            _teamService.Delete(id);
        }

    }
}
