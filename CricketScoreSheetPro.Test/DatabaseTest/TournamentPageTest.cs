using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Service.Implementation;
using CricketScoreSheetPro.Core.ViewModel;
using CricketScoreSheetPro.Test.Extension;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace CricketScoreSheetPro.Test.DatabaseTest
{
    [TestClass]
    public class TournamentPageTest
    {
        private TournamentListViewModel _listViewModel;
        private TestClient Client;

        public TournamentPageTest()
        {
            Client = new TestClient();
            _listViewModel = new TournamentListViewModel(new DataSeedService<Tournament>(Client),
                new DataSeedService<Access>(Client));     
        }

        [TestCleanup]
        public void MethodCleanup()
        {
            new DataSeedService<object>(Client).DeleteDatabase();
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
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Document does not exist.")]
        public void DeleteTournamentTest()
        {
            //Arrange
            var newtournament = _listViewModel.AddTournament("DeleteTournamentTest");

            //Act
            _listViewModel.DeleteTournament(newtournament);

            //Assert
            new DataSeedService<Tournament>(new TestClient()).GetItem(newtournament).Should().BeNull();
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
            existingTournaments.Count(t => t.Name == name).Should().BeGreaterOrEqualTo(1);
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void AddAccessTest()
        {
            //Arrange
            var secondclient = new TestClient();
            secondclient.SetUUID("UUID2");
            var viewmodel = new TournamentListViewModel(new DataSeedService<Tournament>(secondclient), new DataSeedService<Access>(secondclient));
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
            //Arrange
            AddAccessTest();

            //Act 
            var importedTournaments = _listViewModel.ImportedTournaments();

            //Assert
            importedTournaments.Count(t=>t.Name == "ImportTournamanet").Should().BeGreaterOrEqualTo(1);
        }

    }
}
