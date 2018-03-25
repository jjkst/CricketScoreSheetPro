using System;

namespace CricketScoreSheetPro.Core.Model
{
    public class Team
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool ImportedFlg { get; set; }
        public DateTime AddDate { get; set; }

        public Team()
        {
            AddDate = DateTime.Today;
        }
    }
}
