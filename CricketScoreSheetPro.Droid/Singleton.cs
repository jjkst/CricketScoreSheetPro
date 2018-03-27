using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Service.Implementation;
using CricketScoreSheetPro.Core.ViewModel;
using System;

namespace CricketScoreSheetPro.Droid
{
    public class Singleton
    {
        public string UniqueUserId { get; set; }

        #region Singleton

        private static readonly Singleton instance = new Singleton();
        
        private Singleton() { }

        public static Singleton Instance => instance;

        #endregion Singleton                

        #region Tournament

        private TournamentService tournamentService;
        private TournamentService SetTournamentService()
        {
            if (tournamentService == null)
                tournamentService = new TournamentService(new Repository<Tournament>(),
                new Repository<TournamentDetail>());
            return tournamentService;
        }

        private TournamentListViewModel tournamentViewModel;
        public TournamentListViewModel TournamentViewModel()
        {
            //SetTournamentService();
            tournamentViewModel = tournamentViewModel ?? new TournamentListViewModel(tournamentService);
            return tournamentViewModel;
        }

        private TournamentViewModel _tournamentDetailViewModel;
        //public TournamentDetailViewModel TournamentDetailViewModel(string tournamentId)
        //{
        //    if (_tournamentDetailViewModel == null || _tournamentDetailViewModel.Tournament.Id != tournamentId)
        //        _tournamentDetailViewModel = new TournamentDetailViewModel(tournamentService, tournamentId);
        //    return _tournamentDetailViewModel;
        //}

        #endregion Tournament
    }
}