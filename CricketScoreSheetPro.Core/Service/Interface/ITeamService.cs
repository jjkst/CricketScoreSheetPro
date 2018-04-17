using CricketScoreSheetPro.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CricketScoreSheetPro.Core.Service.Interface
{
    public interface ITeamService
    {
        Team AddTeam(string teamName);
        void DeleteTeam(string id);
        IList<Team> GetTeams();
        Team GetTeam(string teamId);
        bool UpdateTeam(Team teamdetail);
    }
}
