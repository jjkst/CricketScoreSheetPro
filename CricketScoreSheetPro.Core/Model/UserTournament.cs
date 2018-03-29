using System;

namespace CricketScoreSheetPro.Core.Model
{
    public class UserTournament
    {
        public string Id { get; set; }
        public string TournamentId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public AccessType AccessType{ get; set; }
        public bool Owner { get; set; }
        public DateTime AddDate { get; set; }
    }
}
