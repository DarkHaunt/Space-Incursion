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
        private readonly Dictionary<PlayerRef, string> _nicknames = new();
        private readonly Dictionary<PlayerRef, int> _scores = new();

        public void AddPlayer(PlayerRef playerRef, string nickName, PlayerNetworkModel model, PlayerUIView playerView)
        {
            _nicknames.Add(playerRef, nickName);
            _scores.Add(playerRef, 0);
            
            _views.Add(playerRef, playerView);
            _models.Add(playerRef, model);
            
            UpdateView(playerRef);
        }

        public void RemovePlayer(PlayerRef playerRef)
        {
            _nicknames.Remove(playerRef);
            _scores.Remove(playerRef);

            _models.Remove(playerRef);
            _views.Remove(playerRef);
        }

        public void UpdateView(PlayerRef playerRef)
        {
            var model = _models[playerRef];
            var view = _views[playerRef];
            
            var nickname = _nicknames[playerRef];
            var score = _scores[playerRef];
            
            view.UpdateScore(score);
            view.UpdateNickname(nickname);
            view.UpdateTextColor(model.PlayerColor);
        }

        public bool IsPlayerAlreadyRegistered(PlayerRef player) =>
            _models.ContainsKey(player);

        public NetworkObject GetPlayerObject(PlayerRef player) =>
            _models[player].Object;

        public PlayerUIView GetPlayerView(PlayerRef player) =>
            _views[player];

        public PlayerRef GetPlayerWithHighestScore()
        {
            return _scores
                .OrderByDescending(kv => kv.Value)
                .First().Key;
        }
    }
}