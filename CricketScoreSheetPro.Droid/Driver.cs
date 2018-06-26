using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Service.Implementation;
using CricketScoreSheetPro.Core.Service.Interface;
using CricketScoreSheetPro.Core.ViewModel;
using System;

namespace CricketScoreSheetPro.Droid
{
    public class Driver
    {
        public Client Client { get; set; } = new Client();
        public static string UniqueUserId { get; set; }

        private IDataSeedService<Access> accessService;


        #region Tournament

        private IDataSeedService<Tournament> tournamentService;
        private IDataSeedService<Tournament> SetTournamentService()
        {
            if (tournamentService == null)
                tournamentService = new DataSeedService<Tournament>(Client);
            return tournamentService;
        }

        private TournamentListViewModel tournamentListViewModel;
        public TournamentListViewModel TournamentListViewModel()
        {
            tournamentService = tournamentService ?? SetTournamentService();
            tournamentListViewModel = tournamentListViewModel ?? new TournamentListViewModel(tournamentService, accessService);
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

        private IDataSeedService<Team> teamService;

        private IDataSeedService<Team> SetTeamService()
        {
            if (teamService == null)
                teamService = new DataSeedService<Team>(Client);
            return teamService;
        }

        private TeamListViewModel teamtListViewModel;
        public TeamListViewModel TeamListViewModel()
        {
            teamService = teamService ?? SetTeamService();
            teamtListViewModel = teamtListViewModel ?? new TeamListViewModel(teamService, accessService);
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

        private IDataSeedService<Umpire> umpireService;
        private IDataSeedService<Umpire> UmpireService()
        {
            if (umpireService == null)
                umpireService = new DataSeedService<Umpire>(Client);
            return umpireService;
        }

        private IDataSeedService<Location> locationService;
        private IDataSeedService<Location> LocationService()
        {
            if (locationService == null)
                locationService = new DataSeedService<Location>(Client);
            return locationService;
        }

        private IDataSeedService<Match> matchService;

        private NewGameViewModel newgameViewModel;
        public NewGameViewModel NewGameViewModel()
        {
            teamService = teamService ?? SetTeamService();
            locationService = locationService ?? LocationService();
            umpireService = umpireService ?? UmpireService();
            if (newgameViewModel == null)
                newgameViewModel = new NewGameViewModel(Client, matchService, teamService, locationService, umpireService);
            return newgameViewModel;
        }

        #endregion New Game
    }
}