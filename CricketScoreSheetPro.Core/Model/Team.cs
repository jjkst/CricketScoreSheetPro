using System;
using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Model
{
    public class Team
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool Owner { get; set; }

        public AccessType AccessType { get; set; }

        public DateTime AddDate { get; set; }

        public IList<string> Players { get; set; }

        public Team()
        {
            Players = new List<string>();
        }
    }
}
