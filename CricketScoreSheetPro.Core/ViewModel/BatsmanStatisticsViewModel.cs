using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CricketScoreSheetPro.Core.ViewModel
{
    public class BatsmanStatisticsViewModel
    {
        private readonly IDataSeedService<PlayerInning> _playerInningService;

        public BatsmanStatisticsViewModel(IDataSeedService<PlayerInning> playerInningService)
        {
            _playerInningService = playerInningService ?? throw new ArgumentNullException($"playerService is null, cannot get match.");
            var playerinnings = _playerInningService.GetList();
            BatsmanStatistics = new PlayerStatistics();
        }

        public PlayerStatistics BatsmanStatistics { get; private set; }
    }
}
