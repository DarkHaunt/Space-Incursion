using System;
using Cysharp.Threading.Tasks;
using Game.Code.Game.Services;
using Game.Code.Game.Level;
using Game.Code.Game.UI;
using Fusion;
using Game.Code.Extensions;
using Game.Code.Game.Entities;
using Game.Code.Game.Scene;

namespace Game.Code.Game
{
    public class NetworkSpawnService
    {
        private readonly SceneDependenciesProvider _sceneDependenciesProvider;
        private readonly PlayerHandleService _playerHandleService;
        private readonly PlayerColorProvider _colorProvider;

        private readonly GameFactory _gameFactory;
        private readonly NetworkRunner _runner;

        private bool IsHost
            => _runner.CanSpawn;

        public NetworkSpawnService(NetworkMonoServiceLocator serviceLocator, SceneDependenciesProvider sceneDependenciesProvider,
            GameFactory gameFactory, PlayerColorProvider colorProvider, PlayerHandleService playerHandleService)
        {
            _sceneDependenciesProvider = sceneDependenciesProvider;
            _playerHandleService = playerHandleService;

            _colorProvider = colorProvider;
            _gameFactory = gameFactory;

            _runner = serviceLocator.Runner;
        }

        public async UniTask<LevelModel> TryToSpawnLevel()
        {
            if (!IsHost)
                return null;

            return await _gameFactory.CreateLevel();
        }

        public async UniTask<UIRoot> TryToSpawnUIRoot() =>
            await _gameFactory.CreateUIRoot(_sceneDependenciesProvider.UIRoot);

        public async UniTask<PlayerNetworkModel> TryToGetPlayerData(PlayerRef playerRef, string nickName)
        {
            if (!IsHost)
            {
                await UniTask.Delay(500); // To wait player be spawned by NetworkRunner

                if (!_runner.TryGetPlayerObject(playerRef, out var obj))
                    throw new Exception($"Player {playerRef} is not in runner");
                
                return obj.GetBehaviour<PlayerNetworkModel>();
            }

            var pos = _sceneDependenciesProvider.PlayerSpawnPoints.PickRandom().position;
            var color = _colorProvider.GetAvailableColor();
            
            var player = await _gameFactory.CreatePlayer(pos, playerRef, nickName, color);

            return player;
        }

        public void TryToDespawnPlayer(PlayerRef player)
        {
            if (!IsHost)
                return;

            if (_runner.TryGetPlayerObject(player, out var behavior))
            {
                _playerHandleService.RemovePlayer(player);
                _runner.Despawn(behavior);
            }
        }
    }
}