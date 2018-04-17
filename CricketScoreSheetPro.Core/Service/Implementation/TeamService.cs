using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Repository.Interface;
using CricketScoreSheetPro.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CricketScoreSheetPro.Core.Service.Implementation
{
    public class TeamService : ITeamService
    {
        private readonly IRepository<Team> _teamRepository;

        public TeamService(IRepository<Team> teamRepository)
        {
            _teamRepository = teamRepository ?? throw new ArgumentNullException($"teamRepository is null");
        }

        public Team AddTeam(string teamName)
        {
            if (string.IsNullOrEmpty(teamName)) throw new ArgumentException($"Team name is empty");
            var newteamproperties = new Dictionary<string, object>
            {
                { "type", nameof(Team)},
                { "value", new Team
                            {
                                Name = teamName,
                                AccessType = AccessType.Moderator,
                                Owner = true,
                                AddDate = DateTime.Today
                            }}
            };
            var teamAdded = _teamRepository.Create(newteamproperties);
            return teamAdded;
        }

        public void DeleteTeam(string teamid)
        {
            if (string.IsNullOrEmpty(teamid)) throw new ArgumentException($"Team ID is null");
            _teamRepository.Delete(teamid);
        }

        public Team GetTeam(string teamId)
        {
            if (string.IsNullOrEmpty(teamId)) throw new ArgumentException($"Team ID is null");
            var team = _teamRepository.GetItem(teamId);
            return team;
        }

        public IList<Team> GetTeams()
        {
            var result = _teamRepository.GetList();
            return result;
        }

        public bool UpdateTeam(Team team)
        {
            if (team == null) throw new ArgumentException($"Team is null");

            //update team
            var updatedteam = _teamRepository.Update(team.Id, team);

            return updatedteam;
        }
    }
}
