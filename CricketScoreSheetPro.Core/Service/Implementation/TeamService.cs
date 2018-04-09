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
        private readonly IRepository<UserTeam> _userteamRepository;
        private readonly IRepository<Team> _teamRepository;

        public TeamService(IRepository<UserTeam> userteamRepository, IRepository<Team> teamRepository)
        {
            _userteamRepository = userteamRepository ?? throw new ArgumentNullException($"userteamRepository is null");
            _teamRepository = teamRepository ?? throw new ArgumentNullException($"teamRepository is null");
        }

        public UserTeam AddTeam(string teamName)
        {
            if (string.IsNullOrEmpty(teamName)) throw new ArgumentException($"Team name is empty");
            var newteamproperties = new Dictionary<string, object>
            {
                { "type", nameof(Team)},
                { "value", new Team
                            {
                                Name = teamName
                            }}
            };
            var teamAdded = _teamRepository.Create(newteamproperties);
            if (teamAdded == null) throw new ArgumentException($"Team add is not successful.");
            var newuserteamproperties = new Dictionary<string, object>
            {
                { "team", teamAdded.Id },
                { "type", nameof(UserTeam)},
                { "value", new UserTeam
                            {
                                TeamId = teamAdded.Id,
                                Name = teamAdded.Name,
                                AccessType = AccessType.Moderator,
                                Owner = true,
                                AddDate = DateTime.Today                                
                            }}
            };
            var userteamAdded = _userteamRepository.Create(newuserteamproperties);

            return userteamAdded;
        }

        public void DeleteTeam(string userteamid)
        {
            if (string.IsNullOrEmpty(userteamid)) throw new ArgumentException($"UserTeam ID is null");
            var userteam = _userteamRepository.GetItem(userteamid);
            if (userteam == null) throw new ArgumentException($"Not able to find user team.");
            _teamRepository.Delete(userteam.TeamId);
            _userteamRepository.Delete(userteam.Id);
        }

        public Team GetTeam(string teamId)
        {
            if (string.IsNullOrEmpty(teamId)) throw new ArgumentException($"Team ID is null");
            var team = _teamRepository.GetItem(teamId);
            return team;
        }

        public IList<UserTeam> GetUserTeams()
        {
            var result = _userteamRepository.GetList();
            return result;
        }

        public bool UpdateTeam(Team team)
        {
            if (team == null) throw new ArgumentException($"Team is null");

            //update team
            var updatedteam = _teamRepository.Update(team.Id, team);

            // update usertournament
            var userteams = _userteamRepository.GetListByProperty("team", team.Id);
            if (userteams == null) throw new ArgumentException($"Not able to find team id in users list");
            if (userteams[0].Name == team.Name) return updatedteam; //No need if no name change

            bool updateduserteam = true;
            foreach (var ut in userteams)
            {
                ut.Name = team.Name;
                updateduserteam = updateduserteam && _userteamRepository.Update(ut.Id, ut);
            }

            return updateduserteam && updateduserteam;
        }
    }
}
