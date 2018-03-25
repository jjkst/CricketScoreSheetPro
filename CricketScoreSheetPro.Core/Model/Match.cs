using System;
using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Model
{
    public class Match
    {
        public string Id { get; set; }

        //Index
        public string TournamentId { get; set; } 

        public DateTime AddDate { get; set; }

        public int TotalOvers { get; set; }

        public string Location { get; set; }

        public bool MatchComplete { get; set; }

        public TeamInning HomeTeam { get; set; }

        public TeamInning AwayTeam { get; set; }

        public string WinningTeamName { get; set; }

        public string Comments { get; set; }

        public string PrimaryUmpire { get; set; }

        public string SecondaryUmpire { get; set; }

        public bool ImportedFlg { get; set; }

        public Match()
        {
            AddDate = DateTime.Today;
        }

    }
}
