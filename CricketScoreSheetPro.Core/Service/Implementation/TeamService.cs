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

        public string AddTeam(Team team)
        {
            if (team == null) throw new ArgumentNullException($"team is null");
            var teamAdded = _teamRepository.Create(team);
            return teamAdded;
        }

        public void DeleteTeam(string teamId)
        {
            if (string.IsNullOrEmpty(teamId)) throw new ArgumentException($"Team ID is null");
            _teamRepository.Delete(teamId);
        }

        public Team GetTeam(string teamId)
        {
            if (string.IsNullOrEmpty(teamId)) throw new ArgumentException($"Team ID is null");
            var team = _teamRepository.GetItem(teamId);
            if (team == null) throw new ArgumentException($"Document does not exist.");
            return team;
        }

        public IList<Team> GetTeamList()
        {
            var result = _teamRepository.GetList();
            return result;
        }

        public bool UpdateTeam(Team team)
        {
            if (team == null) throw new ArgumentException($"Tournament is null");
            return _teamRepository.Update(team.Id, team);
        }
    }
}
