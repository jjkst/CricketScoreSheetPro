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

        public TeamListViewModel(ITeamService teamService)
        {
            _teamService = teamService ?? throw new ArgumentNullException($"TeamService is null, cannot get teams.");
        }

        public List<UserTeam> Teams => _teamService.GetUserTeams().ToList();

        public UserTeam AddTeam(string teamName)
        {
            var newteam = _teamService.AddTeam(teamName);
            return newteam;
        }

        public void DeleteTeam(string id)
        {
            _teamService.DeleteTeam(id);
        }

    }
}
