using System;

namespace CricketScoreSheetPro.Core.Model
{
    public class UserTeam
    {
        public string Id { get; set; }
        public string TeamId { get; set; }
        public string Name { get; set; }
        public bool Owner { get; set; }
        public AccessType AccessType { get; set; }
        public DateTime AddDate { get; set; }
    }
}
