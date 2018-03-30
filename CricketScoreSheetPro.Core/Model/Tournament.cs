using System;
using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Model
{
    public class Tournament
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Sponsor { get; set; }

        public string Status { get; set; }

        public DateTime StartDate { get; set; }        

        public decimal EntryFee { get; set; }

        public IList<string> Prizes { get; set; }

        public IList<string> Facilities { get; set; }

        public IList<string> Venues { get; set; }    
        
        public IList<UserTeam> Teams { get; set; }

        public int TotalOvers { get; set; }

        public Tournament()
        {
            Prizes = new List<string>();
            Facilities = new List<string>();
            Venues = new List<string>();
            Teams = new List<UserTeam>();
        }
    }
}
