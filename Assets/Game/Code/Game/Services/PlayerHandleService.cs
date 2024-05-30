using System.Collections.Generic;
using Game.Code.Game.UI;
using System.Linq;
using Fusion;
using UnityEngine;

namespace Game.Code.Game.Services
{
    public class PlayerHandleService
    {
        private readonly Dictionary<PlayerRef, PlayerUIView> _views = new();
        private readonly Dictionary<PlayerRef, string> _nicknames = new();
        private readonly Dictionary<PlayerRef, int> _scores = new();

        public void AddPlayer(PlayerRef playerRef, string nickName, Color color, PlayerUIView playerView)
        {
            _nicknames.Add(playerRef, nickName);
            _views.Add(playerRef, playerView);
            _scores.Add(playerRef, 0);
            
            UpdateView(playerRef, color);
        }

        public void RemovePlayer(PlayerRef playerRef)
        {
            _nicknames.Remove(playerRef);
            _scores.Remove(playerRef);
            _views.Remove(playerRef);
        }

        public void UpdateView(PlayerRef playerRef, Color color)
        {
            var view = _views[playerRef];
            var score = _scores[playerRef];
            var nickname = _nicknames[playerRef];
            
            view.UpdateScore(score);
            view.UpdateTextColor(color);
            view.UpdateNickname(nickname);
        }

        public PlayerRef GetPlayerWithHighestScore()
        {
            return _scores
                .OrderByDescending(kv => kv.Value)
                .First().Key;
        }
    }
}