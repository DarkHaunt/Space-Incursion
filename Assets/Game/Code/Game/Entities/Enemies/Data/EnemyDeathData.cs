using Fusion;
using Game.Code.Game.Entities.Enemies.Models;
using Game.Code.Game.Services;

namespace Game.Code.Game.Entities.Enemies.Data
{
    public struct EnemyDeathData
    {
        public readonly EnemyNetworkModel Self;
        public readonly PlayerRef Killer;

        public EnemyDeathData(EnemyNetworkModel self, PlayerRef killer)
        {
            Self = self;
            Killer = killer;
        }
    }
}