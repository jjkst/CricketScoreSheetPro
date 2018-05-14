using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Repository.Interface;
using CricketScoreSheetPro.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CricketScoreSheetPro.Core.Service.Implementation
{
    public class PlayerService : IPlayerService
    {
        private readonly IRepository<Player> _playerRepository;
        private readonly IRepository<PlayerInning> _playerinningRepository;

        public PlayerService(IRepository<Player> playerRepository)
        {
            _playerRepository = playerRepository ?? throw new ArgumentNullException($"playerRepository is null");
        }

        public PlayerService(IRepository<PlayerInning> playerinningRepository)
        {
            _playerinningRepository = playerinningRepository ?? throw new ArgumentNullException($"playerinningRepository is null");
        }

        public string AddPlayer(Player player)
        {
            if (player == null) throw new ArgumentNullException($"player is null");
            var playerAdded = _playerRepository.Create(player);
            return playerAdded;
        }

        public void DeletePlayer(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException($"Player ID is null");
            _playerRepository.Delete(id);
        }

        public Player GetPlayer(string playerId)
        {
            if (string.IsNullOrEmpty(playerId)) throw new ArgumentException($"PLayer ID is null");
            var player = _playerRepository.GetItem(playerId);
            if (player == null) throw new ArgumentException($"Document does not exist.");
            return player;
        }

        public IList<Player> GetPlayerList()
        {
            var result = _playerRepository.GetList();
            return result;
        }

        public bool UpdatePlayer(Player player)
        {
            if (player == null) throw new ArgumentException($"Tournament is null");
            return _playerRepository.Update(player.Id, player);
        }

        public string AddPlayerInning(PlayerInning playerinning)
        {
            if (playerinning == null) throw new ArgumentNullException($"player inning is null");
            var playerinningAdded = _playerinningRepository.Create(playerinning);
            return playerinningAdded;
        }

        public void DeletePlayerInning(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException($"Player ID is null");
            _playerinningRepository.Delete(id);
        }

        public PlayerInning GetPlayerInning(string playerinningId)
        {
            if (string.IsNullOrEmpty(playerinningId)) throw new ArgumentException($"Player inning ID is null");
            var playerinning = _playerinningRepository.GetItem(playerinningId);
            if (playerinning == null) throw new ArgumentException($"Document does not exist.");
            return playerinning;
        }

        public IList<PlayerInning> GetPlayerInningList()
        {
            var result = _playerinningRepository.GetList();
            return result;
        }

        public bool UpdatePlayerInning(PlayerInning playerinning)
        {
            if (playerinning == null) throw new ArgumentException($"Tournament is null");
            return _playerinningRepository.Update(playerinning.Id, playerinning);
        }
    }
}
