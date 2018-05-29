using Couchbase.Lite.Query;
using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CricketScoreSheetPro.Core.ViewModel
{
    public class BatsmanStatisticsViewModel
    {
        private readonly IDataSeedService<PlayerInning> _playerInningService;

        public BatsmanStatisticsViewModel(IDataSeedService<PlayerInning> playerInningService, string filter)
        {
            _playerInningService = playerInningService ?? throw new ArgumentNullException($"playerService is null, cannot get match.");

            IExpression condition = Expression.Property("type").EqualTo(Expression.String("PlayerInning"));
            if(filter == "only tournament matches")
                condition.Add(Expression.Property("tournamentId").IsNot(Expression.String(string.Empty)));

            List<PlayerInning> playerinnings = _playerInningService.GetFilteredList(condition).ToList();
            BatsmanStatistics = playerinnings.GroupBy(pi => pi.PlayerId).Select(p => new PlayerStatistics(p.ToList())).ToList();
        }

        public List<PlayerStatistics> BatsmanStatistics { get; private set; }
    }
}
