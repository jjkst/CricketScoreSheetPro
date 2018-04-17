using System;
using System.Linq;
using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Repository.Implementation;
using CricketScoreSheetPro.Core.Service.Implementation;
using CricketScoreSheetPro.Core.ViewModel;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CricketScoreSheetPro.Test.DatabaseTest
{
    [TestClass]
    public class TeamPageTest
    {
        private TeamListViewModel _listViewModel;

        public TeamPageTest()
        {
            var testClient = new TestClient();
            var teamService = new TeamService(new Repository<Team>(testClient));
            _listViewModel = new TeamListViewModel(teamService);
        }

        [TestCleanup]
        public void MethodCleanup()
        {
            var database = new Repository<object>(new TestClient());
            database.DeleteDatabase();
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void GetTeamListTest()
        {
            //Arrange
            _listViewModel.AddTeam("GetTeamListTest");

            //Act           
            var existingTeams = _listViewModel.Teams;

            //Assert
            existingTeams.Should().NotBeNull();
            existingTeams.Count(t=>t.Name == "GetTeamListTest").Should().Be(1);
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void AddTeamTest()
        {
            //Act
            var newteam = _listViewModel.AddTeam("AddTeamTest");

            //Assert
            newteam.Should().NotBeNull();
            newteam.Name.Should().Be("AddTeamTest");
            new Repository<Team>(new TestClient()).GetItem(newteam.Id).Name.Should().Be("AddTeamTest");
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void DeleteTeamTest()
        {
            //Arrange
            var newteam = _listViewModel.AddTeam("DeleteTeamTest");

            //Act
            _listViewModel.DeleteTeam(newteam.Id);

            //Assert
            new Repository<Team>(new TestClient()).GetItem(newteam.Id).Should().BeNull();
        }

    }
}
