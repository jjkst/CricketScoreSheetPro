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
            var hometeamid = new DataSeedService<Team>(Client).Create(new Team { Name = "HomeTeam" });
            var awayteamid = new DataSeedService<Team>(Client).Create(new Team { Name = "AwayTeam" });

            var newgame = new NewGameViewModel(
                Client,
                new DataSeedService<Match>(Client),
                new DataSeedService<Team>(Client),
                new DataSeedService<Location>(Client),
                new DataSeedService<Umpire>(Client));
            var match = newgame.AddMatch("HomeTeam", "AwayTeam", "10", "Richmod,VA","PrimUmpire","SecondUmpire");

            //Act
            var matches = _listViewModel.Matches;

            //Assert
            matches.Count.Should().BeGreaterOrEqualTo(1); 
        }
    }
}
