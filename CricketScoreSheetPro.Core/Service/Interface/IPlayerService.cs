using CricketScoreSheetPro.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CricketScoreSheetPro.Core.Service.Interface
{
    public interface IPlayerService
    {
        string AddPlayer(Player player);
        void DeletePlayer(string id);
        IList<Player> GetPlayerList();
        Player GetPlayer(string playerId);
        bool UpdatePlayer(Player player);

        string AddPlayerInning(PlayerInning player);
        void DeletePlayerInning(string id);
        IList<PlayerInning> GetPlayerInningList();
        PlayerInning GetPlayerInning(string playerId);
        bool UpdatePlayerInning(PlayerInning player);
    }
}
