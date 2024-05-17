using Cysharp.Threading.Tasks;
using Game.Code.Game.Services;
using Game.Code.Game.Entities;
using Game.Code.Game.Level;
using Game.Code.Game.UI;
using UnityEngine;
using Fusion;
using Game.Code.Game.Scene;

namespace Game.Code.Game
{
    public class NetworkHostService
    {
        private readonly SceneDependenciesProvider _sceneDependenciesProvider;
        private readonly GameFactory _gameFactory;
        private readonly NetworkRunner _runner;

        private UIRoot _uiRoot;

        private bool IsHost
            => _runner.CanSpawn;

        public NetworkHostService(NetworkMonoServiceLocator serviceLocator, SceneDependenciesProvider sceneDependenciesProvider, GameFactory gameFactory)
        {
            _sceneDependenciesProvider = sceneDependenciesProvider;
            _gameFactory = gameFactory;

            _runner = serviceLocator.Runner;
        }

        public async UniTask<LevelModel> TryToSpawnLevel()
        {
            if (!IsHost)
                return null;

            return await _gameFactory.CreateLevel();
        }

        public async UniTask<PlayerNetworkModel> TryToSpawnPlayer(PlayerRef playerRef, Vector2 pos, Color color)
        {
            if (!IsHost)
                return null;
            
            var player = await _gameFactory.CreatePlayer(pos, playerRef);
            player.SetColor(color);
            
            Debug.Log($"<color=white>Player Created</color>"); 

            return player;
        }

        public async UniTask<UIRoot> TryToSpawnUIRoot()
        {
            if (!IsHost)
                return null;
            
            return _uiRoot = await _gameFactory.CreateUIRoot(_sceneDependenciesProvider.UIRoot);
        }

        public async UniTask<PlayerUIView> TryToSpawnPlayerUI(Color color, string nickName)
        {
            if (!IsHost)
                return null;
            
            var playerUI = await _gameFactory.CreatePlayerUI(_uiRoot.PlayerViewsContainer);
            
            // TODO: Temporary, locate it in service handling
            playerUI.UpdateNickname(nickName);
            playerUI.UpdateTextColor(color);
            playerUI.UpdateScore(0); 
            
            Debug.Log($"<color=white>Player UI Created</color>"); 

            return playerUI;
        }
        
        public void TryToDespawnObject(PlayerRef player)
        {
            if (!IsHost)
                return;
            
            if (_runner.TryGetPlayerObject(player, out var behavior))
            {
                Debug.Log($"<color=white>Player remove</color>");
                _runner.Despawn(behavior);
            }
        }
    }
}