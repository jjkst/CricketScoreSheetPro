using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Repository.Implementation;
using CricketScoreSheetPro.Core.Service.Implementation;
using CricketScoreSheetPro.Core.ViewModel;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CricketScoreSheetPro.Test.DatabaseTest
{
    [TestClass]
    public class TournamentPageTest
    {
        private TournamentListViewModel _listViewModel;

        public TournamentPageTest()
        {
            var testClient = new TestClient();
            _listViewModel = new TournamentListViewModel(
                new TournamentService(new Repository<Tournament>(testClient)),
                new AccessService(new Repository<Access>(testClient)));     
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void AddTournamentTest()
        {
            //Act
            var newtournament = _listViewModel.AddTournament("AddTournamentTest");

            //Assert
            newtournament.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void GetTournamentListTest()
        {
            //Arrange
            var name = "GetTournamentListTest";
            _listViewModel.AddTournament(name);
            
            //Act           
            var existingTournaments = _listViewModel.Tournaments;

            //Assert
            existingTournaments.Should().NotBeNull();
            existingTournaments.Count(t=>t.Name == name).Should().BeGreaterOrEqualTo(1);
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

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void AddAccessTest()
        {
            //Arrange
            var secondclient = new TestClient();
            secondclient.SetUUID("UUID2");
            var viewmodel = new TournamentListViewModel(
                new TournamentService(new Repository<Tournament>(secondclient)),
                new AccessService(new Repository<Access>(secondclient)));
            var addtournament = viewmodel.AddTournament("ImportTournamanet");

            //Act          
            var access = _listViewModel.AddAccess(addtournament, AccessType.Moderator);

            //Assert
            access.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void ImportedTournamentTest()
        {
            //Act 
            var importedTournaments = _listViewModel.ImportedTournaments();

            //Assert
            importedTournaments.Should().NotBeNull();
        }

    }
}
