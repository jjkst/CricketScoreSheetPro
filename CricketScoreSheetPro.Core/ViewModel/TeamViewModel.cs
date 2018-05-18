using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Service.Interface;
using System;

namespace CricketScoreSheetPro.Core.ViewModel
{
    public class TeamViewModel
    {
        private readonly IDataSeedService<Team> _teamService;

        public TeamViewModel(IDataSeedService<Team> teamService, string teamId)
        {
            _teamService = teamService ?? throw new ArgumentNullException($"TeamService is null, cannot get teams.");
            Team = _teamService.GetItem(teamId) ?? throw new ArgumentNullException($"Team Id is not exist");
        }

        public Team Team { get; private set; }

        public bool UpdateTeam()
        {
            var updated = _teamService.Update(Team.Id, Team);
            return updated;
        }
    }
}
