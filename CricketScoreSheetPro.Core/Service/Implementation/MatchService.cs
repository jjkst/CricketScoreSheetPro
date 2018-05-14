using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Repository.Interface;
using CricketScoreSheetPro.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CricketScoreSheetPro.Core.Service.Implementation
{
    public class MatchService : IMatchService
    {
        private readonly IRepository<Match> _matchRepository;

        public MatchService(IRepository<Match> matchRepository)
        {
            _matchRepository = matchRepository ?? throw new ArgumentNullException($"matchRepository is null");
        }

        public string AddMatch(Match newmatch)
        {
            if (newmatch == null) throw new ArgumentNullException($"newmatch is null");
            var matchAdded = _matchRepository.Create(newmatch);
            return matchAdded;
        }

        public void DeleteMatch(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException($"Match ID is null");
            _matchRepository.Delete(id);
        }

        public Match GetMatch(string matchId)
        {
            if (string.IsNullOrEmpty(matchId)) throw new ArgumentException($"Match ID is null");
            var match = _matchRepository.GetItem(matchId);
            if (match == null) throw new ArgumentException($"Document does not exist.");
            return match;
        }

        public IList<Match> GetMatchList()
        {
            var result = _matchRepository.GetList();
            return result;
        }

        public bool UpdateMatch(Match match)
        {
            if (match == null) throw new ArgumentException($"Match is null");
            return _matchRepository.Update(match.Id, match);
        }
    }
}
