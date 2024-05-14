using Cysharp.Threading.Tasks;
using Game.Code.Game.Services;
using Game.Code.Game.Entities;
using Game.Code.Game.Level;
using UnityEngine;
using Fusion;

namespace Game.Code.Game
{
    public class NetworkHostService
    {
        private readonly GameFactory _gameFactory;
        private readonly NetworkRunner _runner;

        public bool IsHost
            => _runner.Mode == SimulationModes.Host;
        private bool CanSpawn
            => _runner.CanSpawn;
        

        public NetworkHostService(NetworkMonoServiceLocator serviceLocator, GameFactory gameFactory)
        {
            _gameFactory = gameFactory;

            _runner = serviceLocator.Runner;
        }

        public async UniTask<LevelModel> SpawnLevel()
        {
            if (!CanSpawn)
                return null;

            return await _gameFactory.CreateLevel();
        }

        public async UniTask<PlayerNetworkModel> TryToSpawnPlayer(Vector2 pos, PlayerRef playerRef)
        {
            if (!CanSpawn)
                return null;

            Debug.Log($"<color=white>Player Created</color>");
            return await _gameFactory.CreatePlayer(pos, playerRef);
        }

        public void TryToDespawnObject(PlayerRef player)
        {
            if (_runner.TryGetPlayerObject(player, out var behavior))
            {
                Debug.Log($"<color=white>Player remove</color>");
                _runner.Despawn(behavior);
            }
        }
    }
}