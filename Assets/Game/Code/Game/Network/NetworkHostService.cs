using Cysharp.Threading.Tasks;
using Game.Code.Game.Services;
using Game.Code.Game.Level;
using Game.Code.Game.UI;
using UnityEngine;
using Fusion;
using Game.Code.Game.Entities;
using Game.Code.Game.Scene;

namespace Game.Code.Game
{
    public class NetworkHostService
    {
        private readonly SceneDependenciesProvider _sceneDependenciesProvider;
        private readonly PlayerHandleService _playerHandleService;
        private readonly PlayerColorProvider _colorProvider;

        private readonly GameFactory _gameFactory;
        private readonly NetworkRunner _runner;

        private bool IsHost
            => _runner.CanSpawn;

        public NetworkHostService(NetworkMonoServiceLocator serviceLocator, SceneDependenciesProvider sceneDependenciesProvider,
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

        public async UniTask<PlayerNetworkModel> TryToCreatePlayerData(PlayerRef playerRef, PlayerUIView uiView, string nickName, Vector2 pos)
        {
            if (!IsHost)
                return null;

            var playerColor = _colorProvider.GetAvailableColor();
            
            var player = await _gameFactory.CreatePlayer(pos, playerRef, uiView);
            player.RPC_NetworkDataSetUp(playerColor, nickName);

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