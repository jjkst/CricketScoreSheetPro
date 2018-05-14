using CricketScoreSheetPro.Core.Model;
using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Service.Interface
{
    public interface ITeamService
    {
        string AddTeam(Team team);
        void DeleteTeam(string id);
        IList<Team> GetTeamList();
        Team GetTeam(string teamId);
        bool UpdateTeam(Team team);
    }
}
