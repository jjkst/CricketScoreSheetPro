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
        private TournamentListViewModel _listViewModel;
        private TournamentViewModel _viewModel;

        public TournamentPageTest()
        {
            var tournamentService = new TournamentService(new Repository<Tournament>() { UUID = "UUID" },
                new Repository<TournamentDetail>());
            _listViewModel = new TournamentListViewModel(tournamentService);
            _viewModel = new TournamentViewModel(tournamentService);
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
            _listViewModel.AddTournament("GetTournamentListTest", "UUID");
            
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
            var newtournament = _listViewModel.AddTournament("AddTournamentTest", "UUID");

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
            var newtournament = _listViewModel.AddTournament("DeleteTournamentTest", "UUID");
            var beforecount = _listViewModel.Tournaments.Count;

            //Act
            _listViewModel.DeleteTournament(newtournament.Id);
            var aftercount = _listViewModel.Tournaments.Count;

            //Assert
            aftercount.Should().Be(beforecount-1);
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void ImportTournamentTest()
        {
            //Arrange
            var newtournament = _listViewModel.AddTournament("ImportTournamentTest", "UUID2");
            var 

            //Act
            var tournament = _listViewModel.ImportTournament(newtournament.Id);

            //Arrange
            tournament.Should().NotBeNull();
            newtournament.Name.Should().Be("ImportTournamentTest");
        }
    }
}
