using System.Collections.Generic;
using Game.Code.Game.UI;
using System.Linq;
using Fusion;

namespace Game.Code.Game.Services
{
    public class PlayerHandleService
    {
        private readonly Dictionary<PlayerRef, PlayerUIView> _views = new();
        private readonly Dictionary<PlayerRef, string> _nicknames = new();
        private readonly Dictionary<PlayerRef, int> _scores = new();

        public void AddPlayer(PlayerRef playerRef, string nickName, PlayerUIView playerView)
        {
            _nicknames.Add(playerRef, nickName);
            _views.Add(playerRef, playerView);
            _scores.Add(playerRef, 0);
        }

        public void RemovePlayer(PlayerRef playerRef)
        {
            _nicknames.Remove(playerRef);
            _scores.Remove(playerRef);
            _views.Remove(playerRef);
        }

        public void UpdatePlayerScore(PlayerRef playerRef, int newScore)
        {
            _scores[playerRef] = newScore;
        }

        public void UpdatePlayerNickname(PlayerRef playerRef, string playerNickname)
        {
            _nicknames[playerRef] = playerNickname;
        }

        public PlayerRef GetPlayerWithHighestScore()
        {
            return _scores
                .OrderByDescending(kv => kv.Value)
                .First().Key;
        }
    }
}