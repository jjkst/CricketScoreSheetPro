using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CricketScoreSheetPro.Core.ViewModel
{
    public class BatsmanStatisticsViewModel
    {
        private readonly IPlayerService _playerService;

        public BatsmanStatisticsViewModel(IPlayerService playerService)
        {
            _playerService = playerService ?? throw new ArgumentNullException($"playerService is null, cannot get match.");
            var playerinning = _playerService.GetPlayerInningList();
            BatsmanStatistics = new PlayerStatistics();
        }

        public PlayerStatistics BatsmanStatistics { get; private set; }
    }
}
