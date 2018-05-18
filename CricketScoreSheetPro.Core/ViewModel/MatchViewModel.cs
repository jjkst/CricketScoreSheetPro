using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Service.Interface;
using System;

namespace CricketScoreSheetPro.Core.ViewModel
{
    public class MatchViewModel
    {
        private readonly IDataSeedService<Match> _matchService;

        public MatchViewModel(IDataSeedService<Match> matchService, string matchId)
        {
            _matchService = matchService ?? throw new ArgumentNullException($"matchService is null, cannot get match.");
            Match = _matchService.GetItem(matchId) ?? throw new ArgumentNullException($"Match Id is not exist");
        }

        public Match Match { get; private set; }
    }
}
