using CricketScoreSheetPro.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CricketScoreSheetPro.Core.Service.Interface
{
    public interface IPlayerService
    {
        Player AddPlayer(string playerName);
        void DeletePlayer(string id);
        IList<Player> GetPlayers();
        Player GetPlayer(string playerId);
        bool UpdatePlayer(Player playerdetail);
    }
}
