using CricketScoreSheetPro.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CricketScoreSheetPro.Core.Service.Interface
{
    public interface ITeamService
    {
        UserTeam AddTeam(string teamName);
        void DeleteTeam(string id);
        IList<UserTeam> GetUserTeams();
        Team GetTeam(string teamId);
        bool UpdateTeam(Team teamdetail);
    }
}
