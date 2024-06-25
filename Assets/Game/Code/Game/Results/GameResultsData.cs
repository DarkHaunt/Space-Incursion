using System.Collections.Generic;
using Fusion;

namespace Game.Code.Game.Services
{
    public record GameResultsData
    {
        private readonly Dictionary<PlayerRef, int> _playersWithScore;

        public IReadOnlyDictionary<PlayerRef, int> PlayersWithScore =>
            _playersWithScore;

        public GameResultsData(Dictionary<PlayerRef, int> playersWithScore)
        {
            _playersWithScore = playersWithScore;
        }
    }
}