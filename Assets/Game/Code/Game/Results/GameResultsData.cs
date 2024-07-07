using System.Collections.Generic;
using Game.Code.Game.Entities.Player.Data;

namespace Game.Code.Game.Services
{
    public record GameResultsData(Dictionary<NetworkPlayerStaticData, int> PlayersWithScore)
    {
        public Dictionary<NetworkPlayerStaticData, int> PlayersWithScore { get; } = PlayersWithScore;
    }
}