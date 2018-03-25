using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Model
{
    public class Player
    {
        public string Id { get; set; }

        //Index
        public string Name { get; set; }

        public IList<string> Roles { get; set; }
    }

}
