using System;
using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Repository.Implementation;
using CricketScoreSheetPro.Core.Service.Implementation;
using CricketScoreSheetPro.Core.ViewModel;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CricketScoreSheetPro.Test.DatabaseTest
{
    [TestClass]
    public class MatchPageTest
    {
        private MatchListViewModel _listViewModel;
        private TestClient Client;

        public MatchPageTest()
        {
            Client = new TestClient();
            _listViewModel = new MatchListViewModel(
                new MatchService(new Repository<Match>(Client)),
                new AccessService(new Repository<Access>(Client)));
        }

        [TestCleanup]
        public void MethodCleanup()
        {
            new Repository<object>(Client).DeleteDatabase();
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void GetMatchListTest()
        {
            //Arrange
            var newgame = new NewGameViewModel(
                new MatchService(new Repository<Match>(Client)),
                new TeamService(new Repository<Team>(Client)),
                new LocationService(new Repository<Location>(Client)),
                new UmpireService(new Repository<Umpire>(Client)));
            newgame.AddMatch("HomeTeam", "AwayTeam", "10", "Richmod,VA","PrimUmpire","SecondUmpire");

            //Act
            var matches = _listViewModel.Matches;

            //Assert
            matches.Count.Should().BeGreaterOrEqualTo(1); ;
        }
    }
}
