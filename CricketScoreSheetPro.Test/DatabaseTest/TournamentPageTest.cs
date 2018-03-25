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
        private TournamentViewModel _tournamentViewModel;

        public TournamentPageTest()
        {
            var tournamentService = new TournamentService(new Repository<Tournament>(),
                new Repository<TournamentDetail>());
            _tournamentViewModel = new TournamentViewModel(tournamentService);
        }

        [TestMethod]
        public void AddTournamentTest()
        {
            //Act
            var newtournament = _tournamentViewModel.AddTournament("TournamentTest");

            //Assert
            newtournament.Should().NotBeNull();
            newtournament.Name.Should().Be("TournamentTest");
            newtournament.Status.Should().Be("Open");
        }

        [TestMethod]
        public void GetTournamentListTest()
        {
            //Arrange
            _tournamentViewModel.AddTournament("TournamentTest");

            //Act
            var existingTournament = _tournamentViewModel.Tournaments;

            //Assert
            existingTournament.Should().NotBeNull();
            existingTournament.Count.Should().BeGreaterOrEqualTo(1);
        }
    }
}
