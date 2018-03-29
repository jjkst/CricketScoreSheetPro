using System;
using CricketScoreSheetPro.Core.Model;
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

        public TournamentDetailPageTest()
        {
            var tournamentService = new TournamentService(new Repository<Tournament>(),
                new Repository<TournamentDetail>());
            var tournament = tournamentService.AddTournament("TournamentDetailPageTest");
            _viewModel = new TournamentViewModel(tournamentService, tournament.Id);
        }

        [TestCleanup]
        public void MethodCleanup()
        {
            var database = new Repository<object>();
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
            var repo = new Repository<Tournament>().GetItem(tournamentdetail.Id);
            repo.Name.Should().Be(tournamentdetail.Name);
            repo.Status.Should().Be(tournamentdetail.Status);
        }
    }
}
