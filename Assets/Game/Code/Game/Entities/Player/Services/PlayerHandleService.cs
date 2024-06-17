using System.Collections.Generic;
using Game.Code.Game.Entities;
using Game.Code.Game.UI;
using System.Linq;
using Fusion;

namespace Game.Code.Game.Services
{
    public class PlayerHandleService
    {
        private readonly Dictionary<PlayerRef, PlayerNetworkModel> _models = new();
        private readonly Dictionary<PlayerRef, PlayerUIView> _views = new();

        public int PlayersCount =>
            _models.Count;

        public void AddPlayer(PlayerRef playerRef, PlayerNetworkModel model, PlayerUIView playerView)
        {
            _views.Add(playerRef, playerView);
            _models.Add(playerRef, model);
            
            UpdateFullView(playerRef);
        }

        public void RemovePlayer(PlayerRef playerRef)
        {
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

        public PlayerRef GetPlayerWithHighestScore()
        {
            return _models
                .OrderByDescending(kv => kv.Value.Score)
                .First().Key;
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