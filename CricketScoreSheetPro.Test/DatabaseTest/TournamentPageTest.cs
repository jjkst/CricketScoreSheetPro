using Couchbase.Lite;
using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Service.Implementation;
using CricketScoreSheetPro.Core.Service.Interface;
using CricketScoreSheetPro.Core.ViewModel;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace CricketScoreSheetPro.Test.DatabaseTest
{
    [TestClass]
    public class TournamentPageTest
    {
        private TournamentListViewModel _tournamentViewModel;

        public TournamentPageTest()
        {
            var tournamentService = new TournamentService(new Repository<Tournament>() { UUID = "UUID" },
                new Repository<TournamentDetail>());
            _tournamentViewModel = new TournamentListViewModel(tournamentService);            
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
            _tournamentViewModel.AddTournament("GetTournamentListTest", "UUID");
            
            //Act           
            var existingTournaments = _tournamentViewModel.Tournaments;

            //Assert
            existingTournaments.Should().NotBeNull();
            existingTournaments.Count.Should().Be(1);
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void AddTournamentTest()
        {
            //Act
            var newtournament = _tournamentViewModel.AddTournament("AddTournamentTest", "UUID");

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
            var newtournament = _tournamentViewModel.AddTournament("DeleteTournamentTest", "UUID");
            var beforecount = _tournamentViewModel.Tournaments.Count;

            //Act
            _tournamentViewModel.DeleteTournament(newtournament.Id);
            var aftercount = _tournamentViewModel.Tournaments.Count;

            //Assert
            aftercount.Should().Be(beforecount-1);
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void ImportTournamentTest()
        {
            //Arrange
            var newtournament = _tournamentViewModel.AddTournament("ImportTournamentTest", "UUID");

            //Act
            var tournament = _tournamentViewModel.ImportTournament(newtournament.Id);

            //Arrange
            tournament.Should().NotBeNull();
            newtournament.Name.Should().Be("ImportTournamentTest");
        }
    }
}
