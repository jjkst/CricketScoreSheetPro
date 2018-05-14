using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Service.Interface;
using System;

namespace CricketScoreSheetPro.Core.ViewModel
{
    public class TeamViewModel
    {
        private readonly ITeamService _teamService;

        public TeamViewModel(ITeamService teamService, string teamId)
        {
            _teamService = teamService ?? throw new ArgumentNullException($"TeamService is null, cannot get teams.");
            Team = _teamService.GetTeam(teamId) ?? throw new ArgumentNullException($"Team Id is not exist");
        }

        public Team Team { get; private set; }

        public bool UpdateTeam()
        {
            var updated = _teamService.UpdateTeam(Team);
            return updated;
        }
    }
}
