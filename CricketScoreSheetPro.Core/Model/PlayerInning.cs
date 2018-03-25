using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Model
{
    public class PlayerInning
    {
        public string Id { get; set; }

        public string PlayerId { get; set; }

        public string PlayerName { get; set; }

        public string TeamId { get; set; }
       
        public string MatchId { get; set; }

        //Index
        public string Team_MatchId { get; set; } // used for match

        public string TournamentId { get; set; }

        //Index
        public string Player_TournamentId { get; set; }

        public int RunsTaken { get; set; }

        public int BallsPlayed { get; set; }

        public int Fours { get; set; }

        public int Sixes { get; set; }

        public string HowOut { get; set; } = "not out";

        public int RunsGiven { get; set; }

        public int BallsBowled { get; set; }

        public int Wickets { get; set; }

        public int Maiden { get; set; }

        public int NoBalls { get; set; }

        public int Wides { get; set; }

        public int Catches { get; set; }

        public int Stumpings { get; set; }

        public int RunOuts { get; set; }

    }
}
