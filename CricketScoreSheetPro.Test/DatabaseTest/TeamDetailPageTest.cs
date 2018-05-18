using CricketScoreSheetPro.Core.Model;
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
        private TestClient Client;

        public TeamDetailPageTest()
        {
            Client = new TestClient();
            var teamService = new DataSeedService<Team>(Client);
            var teamId = teamService.Create(
                new Team
                {
                    Name = "TeamDetailPageTest"
                });
            _viewModel = new TeamViewModel(teamService, teamId);
        }

        [TestCleanup]
        public void MethodCleanup()
        {
            new DataSeedService<object>(Client).DeleteDatabase();
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
            new DataSeedService<Team>(new TestClient()).GetItem(_viewModel.Team.Id).Name.Should().Be("UpdateTeamName");
        }
    }
}
