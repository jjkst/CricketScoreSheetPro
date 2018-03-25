using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Helper
{
    public class Function
    {
        public static string BallsToOversValueConverter(int balls)
        {
            if (balls < 0) throw new ArgumentException("Balls cannot be negative.");
            return balls / 6 + "." + balls % 6;
        }       
    }
}
