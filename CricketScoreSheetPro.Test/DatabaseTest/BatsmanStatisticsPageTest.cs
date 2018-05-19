using System;
using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Service.Implementation;
using CricketScoreSheetPro.Core.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CricketScoreSheetPro.Test.DatabaseTest
{
    [TestClass]
    public class BatsmanStatisticsPageTest
    {
        private BatsmanStatisticsViewModel _viewModel;
        private TestClient Client;

        public BatsmanStatisticsPageTest()
        {
            Client = new TestClient();            
        }

        [TestCleanup]
        public void MethodCleanup()
        {
            new DataSeedService<object>(Client).DeleteDatabase();
        }

        [TestMethod]
        public void BatsmanStatisticsForAllMatches()
        {
            //Arrange
            _viewModel = new BatsmanStatisticsViewModel(new DataSeedService<PlayerInning>(Client), "all matches");

            //Act
            var d = _viewModel.BatsmanStatistics;
        }
    }
}
