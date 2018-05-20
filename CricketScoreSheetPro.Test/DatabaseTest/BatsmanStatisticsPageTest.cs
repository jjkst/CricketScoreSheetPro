using System;
using System.Collections.Generic;
using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Service.Implementation;
using CricketScoreSheetPro.Core.Service.Interface;
using CricketScoreSheetPro.Core.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CricketScoreSheetPro.Test.DatabaseTest
{
    [TestClass]
    public class BatsmanStatisticsPageTest
    {
        private IDataSeedService<PlayerInning> PlayerInningService;
        private BatsmanStatisticsViewModel _viewModel;
        private TestClient Client;

        public BatsmanStatisticsPageTest()
        {
            Client = new TestClient();
            PlayerInningService = new DataSeedService<PlayerInning>(Client);
            var homeplayerinningId = PlayerInningService.Create(new PlayerInning
            {
                PlayerName = "PlayerName",
            }, new KeyValuePair<string, string>[]
                    {
                        new KeyValuePair<string, string>("matchId", "MatchId"),
                        new KeyValuePair<string, string>("tournamentId", "TournamentId"),
                        new KeyValuePair<string, string>("teamId", "TeamId"),
                        new KeyValuePair<string, string>("playerId", "TeamId:PlayerName")
                    });
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
