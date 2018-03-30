﻿using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Repository.Implementation;
using CricketScoreSheetPro.Core.Service.Implementation;
using CricketScoreSheetPro.Core.ViewModel;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CricketScoreSheetPro.Test.DatabaseTest
{
    [TestClass]
    public class TournamentDetailPageTest
    {
        private TournamentViewModel _viewModel;
        private string UserTournamentId;

        public TournamentDetailPageTest()
        {
            var testClient = new TestClient();
            var tournamentService = new TournamentService(new Repository<UserTournament>(testClient),
                new Repository<Tournament>(testClient));
            var tournament = tournamentService.AddTournament("TournamentDetailPageTest");
            UserTournamentId = tournament.Id;
            _viewModel = new TournamentViewModel(tournamentService, tournament.Id);
        }

        [TestCleanup]
        public void MethodCleanup()
        {
            var database = new Repository<object>(new TestClient());
            database.DeleteDatabase();
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void GetTournamentDetailTest()
        {
            //Act
            var tournamentdetail = _viewModel.Tournament;

            //Assert
            tournamentdetail.Name.Should().Be("TournamentDetailPageTest");
            tournamentdetail.Status.Should().Be("Open");
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void UpdateTournament()
        {
            //Arrange
            var tournamentdetail = _viewModel.Tournament;
            tournamentdetail.Name = "UpdateTournamentName";
            tournamentdetail.Status = "InProgress";

            //Act
            var updated = _viewModel.UpdateTournament(tournamentdetail);

            //Assert
            updated.Should().BeTrue();
            _viewModel.Tournament.Should().Be(tournamentdetail);
            var repo = new Repository<UserTournament>(new TestClient()).GetItem(UserTournamentId);
            repo.Name.Should().Be(tournamentdetail.Name);
            repo.Status.Should().Be(tournamentdetail.Status);
        }
    }
}
