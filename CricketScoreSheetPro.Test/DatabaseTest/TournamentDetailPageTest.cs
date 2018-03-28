using System;
using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Service.Implementation;
using CricketScoreSheetPro.Core.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CricketScoreSheetPro.Test.DatabaseTest
{
    [TestClass]
    public class TournamentDetailPageTest
    {
        private TournamentViewModel _viewModel;

        public TournamentDetailPageTest()
        {
            var tournamentService = new TournamentService(new Repository<Tournament>(),
                new Repository<TournamentDetail>());
            var tournament = tournamentService.AddTournament("TournamentDetailPageTest");
            _viewModel = new TournamentViewModel(tournamentService, tournament.Id);
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
