using System.Diagnostics.CodeAnalysis;

namespace CricketScoreSheetPro.Core.Model
{    
    public class Ball
    {
        public string ActiveBatsmanId { get; set; }

        public string RunnerBatsmanId { get; set; }

        public string RunnerHowOut { get; set; } = "not out";

        public string ActiveBowlerId { get; set; }

        public string FielderId { get; set; }

        public int RunsScored { get; set; }

        public string HowOut { get; set; } = "not out";

        public int Wide { get; set; }

        public int NoBall { get; set; }

        public int Byes { get; set; }

        public int LegByes { get; set; }
    }
}
