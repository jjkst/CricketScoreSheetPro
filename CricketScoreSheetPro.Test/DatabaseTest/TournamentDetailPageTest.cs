using CricketScoreSheetPro.Core.Model;
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
        private TestClient Client;

        public TournamentDetailPageTest()
        {
            Client = new TestClient();
            var tournamentService = new TournamentService(new Repository<Tournament>(Client));
            var tournamentId = tournamentService.AddTournament(
                new Tournament
                {
                    Name = "TournamentDetailPageTest",
                    Status = "Open"
                });
            _viewModel = new TournamentViewModel(tournamentService, tournamentId);
        }

        [TestCleanup]
        public void MethodCleanup()
        {
            new Repository<object>(Client).DeleteDatabase();
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
            _viewModel.Tournament.Name = "UpdateTournamentName";
            _viewModel.Tournament.Status = "InProgress";

            //Act
            var updated = _viewModel.UpdateTournament();

            //Assert
            updated.Should().BeTrue();
            var updatedTournament = new Repository<Tournament>(new TestClient()).GetItem(_viewModel.Tournament.Id);
            updatedTournament.Name.Should().Be("UpdateTournamentName");
            updatedTournament.Status.Should().Be("InProgress");
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void ProvideTournamentAccess()
        {
            //Act
            var access = _viewModel.ProvideAccess(AccessType.Write);

            //Assert
            access.Should().Be($"{_viewModel.Tournament.Id} Write");
        }
    }
}
