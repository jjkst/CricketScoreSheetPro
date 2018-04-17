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

        public Team SelectedAwayTeam { get; set; }        

        public string SelectedTournamentId { get; set; }

        public int TotalOvers { get; set; }

        public Location SelectedLocation { get; set; }

        public Umpire SelectedPrimaryUmpire { get; set; }

        public Umpire SelectedSecondaryUmpire { get; set; }

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

        public Match AddMatch(string hometeamname, string awayteamname, string overs, string location, string primaryumpire, string secondaryumpire)
        {
            return new Match();
        }
    }
}
