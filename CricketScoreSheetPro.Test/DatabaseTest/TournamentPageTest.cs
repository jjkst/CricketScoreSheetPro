using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Repository.Interface;
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
            var tournamentService = new TournamentService(new Repository<Tournament>(),
                new Repository<TournamentDetail>());
            _listViewModel = new TournamentListViewModel(tournamentService);            
        }

        [TestCleanup]
        public void MethodCleanup()
        {
            var database = new Repository<object>();
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
            newtournament.Name.Should().Be("AddTournamentTest");
            newtournament.Status.Should().Be("Open");
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void DeleteTournamentTest()
        {
            //Arrange
            var newtournament = _listViewModel.AddTournament("DeleteTournamentTest");
            var beforecount = _listViewModel.Tournaments.Count;

            //Act
            _listViewModel.DeleteTournament(newtournament.Id);
            var aftercount = _listViewModel.Tournaments.Count;

            //Assert
            aftercount.Should().Be(beforecount-1);
        }

        public class FakeRepo : Repository<Tournament>
        {
            public override string GetUUID()
            {
                return "UUID2";
            }
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void ImportTournamentTest()
        {
            //Arrange
            var tournamentService = new TournamentService(new FakeRepo(),
                new Repository<TournamentDetail>());
            var newtournament = _listViewModel.AddTournament("ImportTournamentTest");

            //Act
            var importtournament = _listViewModel.ImportTournament($"{newtournament.Id} View", "UUID");

            //Arrange
            importtournament.Should().NotBeNull();
            importtournament.Name.Should().Be("ImportTournamentTest");
            importtournament.Status.Should().Be(newtournament.Status);
            importtournament.Id.Should().Be(newtournament.Id);
        }
    }
}
