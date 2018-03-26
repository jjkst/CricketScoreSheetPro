using CricketScoreSheetPro.Core.Service.Implementation;
using CricketScoreSheetPro.Core.ViewModel;

namespace CricketScoreSheetPro.Droid
{
    public class Singleton
    {
        public string UniqueUserId { get; set; } = "UUID";

        #region Singleton

        private static readonly Singleton instance = new Singleton();
        
        private Singleton() { }

        public static Singleton Instance => instance;

        #endregion Singleton                

        #region Tournament

        private TournamentService tournamentService;
        //private TournamentService SetTournamentService()
        //{
        //    if (tournamentService == null)
        //        tournamentService = new TournamentService(new TournamentRepository(_client, UniqueUserId), 
        //            new TournamentDetailRepository(_client));
        //    return tournamentService;
        //}

        private TournamentViewModel tournamentViewModel;
        public TournamentViewModel TournamentViewModel()
        {
            //SetTournamentService();
            tournamentViewModel = tournamentViewModel ?? new TournamentViewModel(tournamentService);
            return tournamentViewModel;
        }

        private TournamentDetailViewModel _tournamentDetailViewModel;
        //public TournamentDetailViewModel TournamentDetailViewModel(string tournamentId)
        //{
        //    if (_tournamentDetailViewModel == null || _tournamentDetailViewModel.Tournament.Id != tournamentId)
        //        _tournamentDetailViewModel = new TournamentDetailViewModel(tournamentService, tournamentId);
        //    return _tournamentDetailViewModel;
        //}

        #endregion Tournament
    }
}