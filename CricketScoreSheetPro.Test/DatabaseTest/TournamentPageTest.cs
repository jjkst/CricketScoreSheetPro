using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Repository.Implementation;
using CricketScoreSheetPro.Core.Service.Implementation;
using CricketScoreSheetPro.Core.ViewModel;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CricketScoreSheetPro.Test.DatabaseTest
{
    [TestClass]
    public class TournamentPageTest
    {
        private TournamentListViewModel _listViewModel;

        public TournamentPageTest()
        {
            var testClient = new TestClient();
            var tournamentService = new TournamentService(new Repository<Tournament>(testClient));
            var accessService = new AccessService(new Repository<Access>(testClient));
            _listViewModel = new TournamentListViewModel(tournamentService, accessService);            
        }

        [TestCleanup]
        public void MethodCleanup()
        {
            var database = new Repository<object>(new TestClient());
            database.DeleteDatabase();
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void GetTournamentListTest()
        {
            //Arrange
            _listViewModel.AddTournament("GetTournamentListTest");
            
            //Act           
            var existingTournaments = _listViewModel.Tournaments;

            //Assert
            existingTournaments.Should().NotBeNull();
            existingTournaments.Count.Should().Be(1);
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void AddTournamentTest()
        {
            //Act
            var newtournament = _listViewModel.AddTournament("AddTournamentTest");

            //Assert
            newtournament.Should().NotBeNull();
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void DeleteTournamentTest()
        {
            //Arrange
            var newtournament = _listViewModel.AddTournament("DeleteTournamentTest");

            //Act
            _listViewModel.DeleteTournament(newtournament);
        }
    }
}
