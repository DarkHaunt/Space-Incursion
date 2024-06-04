using Cysharp.Threading.Tasks;
using Game.Code.Game.Services;
using Game.Code.Game.Level;
using Game.Code.Game.UI;
using Fusion;
using Game.Code.Extensions;
using Game.Code.Game.Entities;
using Game.Code.Game.Scene;
using Game.Code.Game.StaticData.Indents;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Code.Game
{
    public class NetworkSpawnService
    {
        private readonly SceneDependenciesProvider _sceneDependenciesProvider;
        private readonly PlayerHandleService _playerHandleService;
        private readonly PlayerColorProvider _colorProvider;

        private readonly GameFactory _gameFactory;
        private readonly NetworkRunner _runner;

        public bool IsHost
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

        public async UniTask<LevelModel> SpawnLevel() =>
            await _gameFactory.CreateLevel();

        public async UniTask<UIRoot> SpawnUIRoot() =>
            await _gameFactory.CreateUIRoot(_sceneDependenciesProvider.UIRoot);

        public async UniTask<EnemyNetworkModel> CreateEnemy(Vector2 pos) =>
            await _gameFactory.CreateEnemy(pos);

        public async UniTask<PlayerNetworkModel> SetUpPlayerData(PlayerRef playerRef, string nickName)
        {
            var model = IsHost
                ? await CreatePlayer(playerRef, nickName)
                : await GetExistedPlayer(playerRef);

            var view = await _gameFactory.CreatePlayerUI();
            
            RegisterPlayer(playerRef, nickName, model, view);
            
            return model;
        }

        private UniTask<PlayerNetworkModel> CreatePlayer(PlayerRef playerRef, string nickName)
        {
            var pos = _sceneDependenciesProvider.PlayerSpawnPoints.PickRandom().position;
            var color = _colorProvider.GetAvailableColor();

            return _gameFactory.CreatePlayer(pos, playerRef, nickName, color);
        }

        private async UniTask<PlayerNetworkModel> GetExistedPlayer(PlayerRef playerRef)
        {
            await UniTask.WaitUntil(() => _runner.TryGetPlayerObject(playerRef, out _))
                .Timeout(NetworkIndents.ClientObjectSearchTimeout);
            
            var obj = _runner.GetPlayerObject(playerRef);
            return obj.GetBehaviour<PlayerNetworkModel>();
        }

        private void RegisterPlayer(PlayerRef playerRef, string nickName, PlayerNetworkModel model, PlayerUIView view)
        {
            _playerHandleService.AddPlayer(playerRef, model, view);

            _runner.SetPlayerObject(playerRef, model.Object);
            _runner.SetIsSimulated(model.Object, true);
        }

        public void DespawnPlayer(PlayerRef player)
        {
            if (IsHost)
            {
                var obj = _playerHandleService.GetPlayerObject(player);
                _runner.Despawn(obj);
            }

            var view = _playerHandleService.GetPlayerView(player);
            _playerHandleService.RemovePlayer(player);

            Object.Destroy(view.gameObject);
        }
    }
}