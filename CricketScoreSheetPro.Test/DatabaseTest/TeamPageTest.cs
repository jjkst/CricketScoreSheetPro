using System;
using System.Linq;
using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Service.Implementation;
using CricketScoreSheetPro.Core.ViewModel;
using CricketScoreSheetPro.Test.Extension;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CricketScoreSheetPro.Test.DatabaseTest
{
    [TestClass]
    public class TeamPageTest
    {
        private TeamListViewModel _listViewModel;
        private TestClient Client;

        public TeamPageTest()
        {
            Client = new TestClient();
            _listViewModel = new TeamListViewModel(new DataSeedService<Team>(Client), new DataSeedService<Access>(Client));
        }

        [TestCleanup]
        public void MethodCleanup()
        {
            new DataSeedService<object>(Client).DeleteDatabase();
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void AddTeamTest()
        {
            //Act
            var newteam = _listViewModel.AddTeam("AddTeamTest");

            //Assert
            newteam.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void GetTeamListTest()
        {
            //Arrange
            var name = "GetTeamListTest";
            _listViewModel.AddTeam(name);

            //Act           
            var existingTeams = _listViewModel.Teams;

            //Assert
            existingTeams.Should().NotBeNullOrEmpty();
            existingTeams.Count(t => t.Name == name).Should().BeGreaterOrEqualTo(1);
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Document does not exist.")]
        public void DeleteTeamTest()
        {
            //Arrange
            var newteam = _listViewModel.AddTeam("DeleteTeamTest");

            //Act
            _listViewModel.DeleteTeam(newteam);

            //Assert
            new DataSeedService<Team>(new TestClient()).GetItem(newteam).Should().BeNull();
        }

    }
}
