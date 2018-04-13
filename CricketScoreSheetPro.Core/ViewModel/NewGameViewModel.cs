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

        public List<UserTeam> Teams { get; set; }

        public List<Location> Locations { get; set; }

        public List<Umpire> Umpires { get; set; }

        public NewGameViewModel(ITeamService teamService, ILocationService locationService, IUmpireService umpireService)
        {
            Teams = teamService.GetUserTeams().ToList();
            Locations = locationService.GetLocations().ToList();
            Umpires = umpireService.GetUmpires().ToList();
        }

        public TeamInning SelectedHomeTeam { get; set; }

        public TeamInning SelectedAwayTeam { get; set; }        

        public string SelectedTournamentId { get; set; }

        public int TotalOvers { get; set; }

        public Location SelectedLocation { get; set; }

        public Umpire SelectedPrimaryUmpire { get; set; }

        public Umpire SelectedSecondaryUmpire { get; set; }

        public Match AddMatch()
        {
            return new Match();
        }

    }
}
