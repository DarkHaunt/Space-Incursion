using Game.Code.Game.StaticData.Indents;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Code.Game.Services;
using Game.Code.Game.Entities;
using Game.Code.Extensions;
using Game.Code.Game.Scene;
using Game.Code.Game.UI;
using UnityEngine;
using Fusion;

namespace Game.Code.Game
{
    public class NetworkPlayerHandleService
    {
        private readonly Queue<PlayerRef> _playersToSpawn = new();
        
        private readonly NetworkHostStateHandleService _hostStateHandleService;
        private readonly SceneDependenciesProvider _sceneDependenciesProvider;
        private readonly PlayerHandleService _playerHandleService;
        private readonly NetworkPlayerDataProvider _dataProvider;
        private readonly PlayerColorProvider _colorProvider;
        private readonly GameFactory _gameFactory;
        private readonly NetworkRunner _runner;
        
        public IEnumerable<PlayerRef> PlayersToSpawn =>
            _playersToSpawn;


        public NetworkPlayerHandleService(NetworkHostStateHandleService hostStateHandleService, PlayerHandleService playerHandleService, 
            NetworkMonoServiceLocator serviceLocator, NetworkPlayerDataProvider dataProvider, SceneDependenciesProvider sceneDependenciesProvider, 
            PlayerColorProvider colorProvider, GameFactory gameFactory)
        {
            _hostStateHandleService = hostStateHandleService;

            _sceneDependenciesProvider = sceneDependenciesProvider;
            _playerHandleService = playerHandleService;
            _colorProvider = colorProvider;
            _dataProvider = dataProvider;
            _gameFactory = gameFactory;
            
            _runner = serviceLocator.Runner;
        }
        
        public void AddPlayerToSpawnQueue(PlayerRef playerRef) =>
            _playersToSpawn.Enqueue(playerRef);
        
        public async UniTask SetUpPlayerData(PlayerRef playerRef)
        {
            var model = _hostStateHandleService.IsHost
                ? await CreatePlayer(playerRef, _dataProvider.PlayerData.Nickname)
                : await GetExistedPlayer(playerRef);

            var view = await _gameFactory.CreatePlayerUI();
            
            RegisterPlayer(playerRef, model, view);
            
            model.Construct(_playerHandleService, _gameFactory);
            
            if (model.Runner.LocalPlayer == playerRef)
                _sceneDependenciesProvider.CameraService.SetTarget(model.transform);
            
            _playersToSpawn.Dequeue();
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

        private void RegisterPlayer(PlayerRef playerRef, PlayerNetworkModel model, PlayerUIView view)
        {
            _playerHandleService.AddPlayer(playerRef, model, view);

            _runner.SetPlayerObject(playerRef, model.Object);
            _runner.SetIsSimulated(model.Object, true);
        }
        
        public void DespawnPlayer(PlayerRef player)
        {
            if (_hostStateHandleService.IsHost)
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