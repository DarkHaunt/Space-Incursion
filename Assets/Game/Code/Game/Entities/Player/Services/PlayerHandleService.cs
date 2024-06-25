using Game.Code.Game.Entities.Player.Models;
using Game.Code.Game.Entities.Player.Data;
using System.Collections.Generic;
using Game.Code.Game.UI;
using System.Linq;
using System;
using Fusion;
using UniRx;

namespace Game.Code.Game.Entities.Player.Services
{
    public class PlayerHandleService
    {
        private readonly ReactiveDictionary<PlayerRef, PlayerNetworkModel> _models = new();
        private readonly ReactiveDictionary<PlayerRef, PlayerUIView> _views = new();

        private readonly ReactiveCollection<PlayerRef> _alivePlayers = new();

        public int AlivePlayersCount =>
            _alivePlayers.Count;

        public IObservable<CollectionRemoveEvent<PlayerRef>> OnPlayerKilled =>
            _alivePlayers.ObserveRemove();

        public void AddPlayer(PlayerRef playerRef, PlayerNetworkModel model, PlayerUIView playerView)
        {
            _views.Add(playerRef, playerView);
            _models.Add(playerRef, model);
            _alivePlayers.Add(playerRef);

            UpdateFullView(playerRef);
        }

        public void RemovePlayerFromAliveList(PlayerRef player) =>
            _alivePlayers.Remove(player);

        public void RemovePlayer(PlayerRef playerRef)
        {
            if (_alivePlayers.Contains(playerRef))
                _alivePlayers.Remove(playerRef);

            _models.Remove(playerRef);
            _views.Remove(playerRef);
        }

        public void IncreasePlayerScore(PlayerRef playerRef)
        {
            var model = _models[playerRef];
            model.IncreaseScore();
        }

        public void UpdatePlayerScoreView(PlayerRef playerRef, int score)
        {
            var view = _views[playerRef];
            view.UpdateScore(score);
        }

        public bool IsPlayerAlreadyRegistered(PlayerRef player) =>
            _models.ContainsKey(player);

        public NetworkObject GetPlayerObject(PlayerRef player) =>
            _models[player].Object;

        public PlayerUIView GetPlayerView(PlayerRef player) =>
            _views[player];

        public Dictionary<NetworkPlayerStaticData, int> GetAllPlayersScores()
        {
            return _models.ToDictionary
            (
                keySelector: x => x.Value.Data,
                elementSelector: x => x.Value.Score
            );
        }

        private void UpdateFullView(PlayerRef playerRef)
        {
            var model = _models[playerRef];
            var view = _views[playerRef];

            var data = model.Data;

            view.UpdateScore(model.Score);
            view.UpdateTextColor(data.Color);
            view.UpdateNickname(data.Nickname);
        }
    }
}