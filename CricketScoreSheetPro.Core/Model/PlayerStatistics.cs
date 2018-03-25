using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Model
{
    public class PlayerStatistics
    {
        public string PlayerName { get; set; }

        public string TeamName { get; set; }

        public int Matches { get; set; }

        public int Innings { get; set; }

        public int NotOuts { get; set; }

        public int Runs { get; set; }

        public int HS { get; set; }

        public int Balls { get; set; }

        public int Hundreds { get; set; }

        public int Fifties { get; set; }

        public decimal BattingAvg { get; set; }

        public decimal SR { get; set; }

        public int BallsBowled { get; set; }

        public int Maiden { get; set; }

        public int RunsGiven { get; set; }

        public int Wickets { get; set; }

        public string BB { get; set; }

        public int FWI { get; set; }

        public int TWI { get; set; }

        public decimal BowlingAvg { get; set; }

        public decimal Econ { get; set; }

        public decimal BowlingSR { get; set; }

        public int Catches { get; set; }

        public int Stumpings { get; set; }

    }
}
