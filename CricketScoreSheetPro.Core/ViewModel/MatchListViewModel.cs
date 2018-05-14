using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CricketScoreSheetPro.Core.ViewModel
{
    public class MatchListViewModel
    {
        private readonly IMatchService _matchService;
        private readonly IAccessService _accessService;

        public MatchListViewModel(IMatchService matchService, IAccessService accessService)
        {
            _matchService = matchService ?? throw new ArgumentNullException($"MatchService is null, cannot get tournaments.");
            _accessService = accessService ?? throw new ArgumentNullException($"AccessService is null, cannot get tournaments.");
        }

        public List<Match> Matches => _matchService.GetMatchList().ToList();

        public List<Match> ImportedMatches()
        {
            var access = _accessService.GetAccessList().FirstOrDefault(a => a.DocumentType == nameof(Match));
            var importedMatches = new List<Match>();
            foreach (var t in access.Documents)
            {
                importedMatches.Add(_matchService.GetMatch(t.Id));
            }
            return importedMatches;
        }
    }
}
