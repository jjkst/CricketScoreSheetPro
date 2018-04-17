using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Repository.Implementation;
using CricketScoreSheetPro.Core.Service.Implementation;
using CricketScoreSheetPro.Core.ViewModel;
using System;

namespace CricketScoreSheetPro.Droid
{
    public class Driver
    {
        public Client Client { get; set; } = new Client();
        public static string UniqueUserId { get; set; }           

        #region Tournament

        private TournamentService tournamentService;
        private TournamentService SetTournamentService()
        {
            if (tournamentService == null)
                tournamentService = new TournamentService(new Repository<UserTournament>(Client),
                new Repository<Tournament>(Client));
            return tournamentService;
        }

        private TournamentListViewModel tournamentListViewModel;
        public TournamentListViewModel TournamentListViewModel()
        {
            tournamentService = tournamentService ?? SetTournamentService();
            tournamentListViewModel = tournamentListViewModel ?? new TournamentListViewModel(tournamentService);
            return tournamentListViewModel;
        }

        private TournamentViewModel tournamentViewModel;
        public TournamentViewModel TournamentViewModel(string tournamentId)
        {
            tournamentService = tournamentService ?? SetTournamentService();
            if (tournamentViewModel == null || tournamentViewModel.Tournament.Id != tournamentId)
                tournamentViewModel = new TournamentViewModel(tournamentService, tournamentId);
            return tournamentViewModel;
        }

        #endregion Tournament

        #region Team

        private TeamService teamService;
        private TeamService SetTeamService()
        {
            if (teamService == null)
                teamService = new TeamService(new Repository<Team>(Client));
            return teamService;
        }

        private TeamListViewModel teamtListViewModel;
        public TeamListViewModel TeamListViewModel()
        {
            teamService = teamService ?? SetTeamService();
            teamtListViewModel = teamtListViewModel ?? new TeamListViewModel(teamService);
            return teamtListViewModel;
        }

        private TeamViewModel teamViewModel;
        public TeamViewModel TeamViewModel(string teamId)
        {
            teamService = teamService ?? SetTeamService();
            if (teamViewModel == null || teamViewModel.Team.Id != teamId)
                teamViewModel = new TeamViewModel(teamService, teamId);
            return teamViewModel;
        }

        #endregion Team

        #region New Game

        private UmpireService umpireService;
        private UmpireService UmpireService()
        {
            if (umpireService == null)
                umpireService = new UmpireService(new Repository<Umpire>(Client));
            return umpireService;
        }

        private LocationService locationService;
        private LocationService LocationService()
        {
            if (locationService == null)
                locationService = new LocationService(new Repository<Location>(Client));
            return locationService;
        }

        private NewGameViewModel newgameViewModel;
        public NewGameViewModel NewGameViewModel()
        {
            teamService = teamService ?? SetTeamService();
            locationService = locationService ?? LocationService();
            umpireService = umpireService ?? UmpireService();
            if (newgameViewModel == null)
                newgameViewModel = new NewGameViewModel(teamService, locationService, umpireService);
            return newgameViewModel;
        }

        #endregion New Game
    }
}