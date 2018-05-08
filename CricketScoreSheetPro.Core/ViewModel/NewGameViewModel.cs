using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CricketScoreSheetPro.Core.ViewModel
{
    public class NewGameViewModel
    {
        public bool IsTournament { get; set; }

        private readonly ITeamService _teamService;

        private readonly ILocationService _locationService;

        private readonly IUmpireService _umpireService;

        public List<Team> Teams => _teamService.GetTeams().ToList();

        public List<Location> Locations => _locationService.GetLocations().ToList();

        public List<Umpire> Umpires => _umpireService.GetUmpires().ToList();

        public NewGameViewModel(ITeamService teamService, ILocationService locationService, IUmpireService umpireService)
        {
            this._teamService = teamService;
            this._locationService = locationService;
            this._umpireService = umpireService;
        }  

        public Umpire AddUmpire(string umpirename)
        {
            return _umpireService.AddUmpire(umpirename);
        }

        public Location AddLocation(string locationname)
        {
            return _locationService.AddLocation(locationname);
        }

        public Team AddTeam(string teamname)
        {
            return _teamService.AddTeam(teamname);
        }

        public Match AddMatch(string hometeamname, string awayteamname, string overs_tournaments, string location, string primaryumpire, string secondaryumpire)
        {
            var hometeam = Teams.FirstOrDefault(n => n.Name == hometeamname);
            if (hometeam == null) throw new NullReferenceException("Home team name is not added");
            var awayteam = Teams.FirstOrDefault(n => n.Name == awayteamname);
            if (awayteam == null) throw new NullReferenceException("Away team name is not added");

            var hometeaminning = new TeamInning
            {
                TeamId = hometeam.Id,
                TeamName = hometeam.Name,
                Team_TournamentId = "",
                TournamentId = "",
                MatchId = "",
            };
            var awayteaminning = new TeamInning
            {
                TeamId = awayteam.Id,
                TeamName = awayteam.Name,
                Team_TournamentId = "",
                TournamentId = "",
                MatchId = "",
            };

            var tournamentId = "";

            var match = new Match
            {
                HomeTeam = hometeaminning,
                AddDate = DateTime.Now,
                AwayTeam = awayteaminning,
                Location = location,
                PrimaryUmpire = primaryumpire,
                SecondaryUmpire = secondaryumpire,
                TotalOvers = int.Parse(overs_tournaments)
            };
            

            return match;
        }
    }
}
