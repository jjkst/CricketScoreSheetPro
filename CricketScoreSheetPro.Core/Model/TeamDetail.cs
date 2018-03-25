using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Model
{
    public class TeamDetail
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IList<Player> Players { get; set; }

        public TeamDetail()
        {
            Players = new List<Player>();
        }
    }
}
