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
            _viewModel.Team.Name = "UpdateTeamName";

            //Act
            var updated = _viewModel.UpdateTeam();

            //Assert
            updated.Should().BeTrue();
            var repo = new Repository<UserTeam>(new TestClient()).GetItem(UserTeamId);
            repo.Name.Should().Be("UpdateTeamName");
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void AddPlayerSaved()
        {
            //Arrange
            _viewModel.AddPlayers("AddPlayer");

            //Act
            _viewModel.UpdateTeam();

            //Assert
            var repo = new Repository<Team>(new TestClient()).GetItem(_viewModel.Team.Id);
            repo.Players.Should().Contain("AddPlayer");
        }
    }
}
