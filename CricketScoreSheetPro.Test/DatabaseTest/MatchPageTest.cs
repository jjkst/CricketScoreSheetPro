using CricketScoreSheetPro.Core.Model;
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
            _listViewModel = new MatchListViewModel(new DataSeedService<Match>(Client), new DataSeedService<Access>(Client));
        }

        [TestCleanup]
        public void MethodCleanup()
        {
            new DataSeedService<object>(Client).DeleteDatabase();
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void GetMatchListTest()
        {
            //Arrange
            var newgame = new NewGameViewModel(
                Client,
                new DataSeedService<Match>(Client),
                new DataSeedService<Team>(Client),
                new DataSeedService<Location>(Client),
                new DataSeedService<Umpire>(Client));
            newgame.AddMatch("HomeTeam", "AwayTeam", "10", "Richmod,VA","PrimUmpire","SecondUmpire");

            //Act
            var matches = _listViewModel.Matches;

            //Assert
            matches.Count.Should().BeGreaterOrEqualTo(1); ;
        }
    }
}
