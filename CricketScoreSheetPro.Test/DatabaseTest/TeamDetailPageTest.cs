using System;
using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Repository.Implementation;
using CricketScoreSheetPro.Core.Service.Implementation;
using CricketScoreSheetPro.Core.ViewModel;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CricketScoreSheetPro.Test.DatabaseTest
{
    [TestClass]
    public class TeamDetailPageTest
    {
        private TeamViewModel _viewModel;
        private string UserTeamId;

        public TeamDetailPageTest()
        {
            var testClient = new TestClient();
            var teamService = new TeamService(new Repository<UserTeam>(testClient),
                new Repository<Team>(testClient));
            var team = teamService.AddTeam("TeamDetailPageTest");
            UserTeamId = team.Id;
            _viewModel = new TeamViewModel(teamService, team.Id);
        }

        [TestCleanup]
        public void MethodCleanup()
        {
            var database = new Repository<object>(new TestClient());
            database.DeleteDatabase();
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void GetTeamDetailTest()
        {
            //Act
            var teamdetail = _viewModel.Team;

            //Assert
            teamdetail.Name.Should().Be("TeamDetailPageTest");
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void UpdateTeam()
        {
            //Arrange
            var teamdetail = _viewModel.Team;
            teamdetail.Name = "UpdateTeamName";

            //Act
            var updated = _viewModel.UpdateTeam(teamdetail);

            //Assert
            updated.Should().BeTrue();
            _viewModel.Team.Should().BeEquivalentTo(teamdetail);
            var repo = new Repository<UserTournament>(new TestClient()).GetItem(UserTeamId);
            repo.Name.Should().Be(teamdetail.Name);
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void AddPlayerSaved()
        {
            //Arrange
            var teamdetail = _viewModel.Team;

            //Act
            _viewModel.AddPlayers("AddPlayer");
            _viewModel.UpdateTeam(_viewModel.Team);

            //Assert
            _viewModel.Team.Should().Be(teamdetail);
            var repo = new Repository<Team>(new TestClient()).GetItem(teamdetail.Id);
            repo.Should().BeEquivalentTo(teamdetail);
        }
    }
}
