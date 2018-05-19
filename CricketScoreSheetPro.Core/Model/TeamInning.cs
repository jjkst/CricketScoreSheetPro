namespace CricketScoreSheetPro.Core.Model
{
    public class TeamInning
    {
        public string Id { get; set; }

        public string TeamId { get; set; }

        public string TeamName { get; set; }

        public string MatchId { get; set; }

        public string TournamentId { get; set; }

        public int Runs { get; set; }

        public int Wickets { get; set; }

        public int Balls { get; set; }

        public int NoBalls { get; set; }

        public int Wides { get; set; }

        public int Byes { get; set; }

        public int LegByes { get; set; }

        public bool Complete { get; set; }
    }
}
