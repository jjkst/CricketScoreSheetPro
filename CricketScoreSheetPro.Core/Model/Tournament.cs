using System;

namespace CricketScoreSheetPro.Core.Model
{
    public class Tournament
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public bool Owner { get; set; }
        public DateTime AddDate { get; set; }
        public Tournament()
        {
            AddDate = DateTime.Today;
        }
    }
}
