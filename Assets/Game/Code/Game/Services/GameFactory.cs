using Game.Code.Game.StaticData;
using Game.Code.Game.Entities;
using Cysharp.Threading.Tasks;
using Game.Code.Game.UI;
using UnityEngine;
using Fusion;
using Game.Code.Game.Entities.Enemies.Models;
using Game.Code.Game.Entities.Player.Data;
using Game.Code.Game.Entities.Player.Models;
using Game.Code.Game.Entities.Projectiles;
using Game.Code.Game.Scene;
using Game.Code.Game.Scene.Level;
using Game.Code.Infrastructure.AssetManaging;
using Game.Code.Infrastructure.Network;
using static Game.Code.Game.StaticData.Indents.AddressableIndents;

namespace Game.Code.Game.Services
{
    public class GameFactory
    {
        private readonly SceneDependenciesProvider _sceneDependenciesProvider;
        private readonly GameStaticDataProvider _dataProvider;
        private readonly AssetProvider _assetProvider;

        private readonly NetworkRunner _runner;

        private UIRoot _uiRoot;


        public GameFactory(AssetProvider assetProvider, GameStaticDataProvider dataProvider, 
            SceneDependenciesProvider sceneDependenciesProvider, NetworkMonoServiceLocator networkServiceLocator)
        {
            _sceneDependenciesProvider = sceneDependenciesProvider;
            _assetProvider = assetProvider;
            _dataProvider = dataProvider;

            _runner = networkServiceLocator.Runner;
        }
        
        public async UniTask<PlayerNetworkModel> CreatePlayer(Vector2 pos, PlayerRef player, string nickName, Color color)
        {
            var prefab = await _assetProvider.LoadAndGetComponent<PlayerNetworkModel>(PlayerAssetPath);
            var obj = await _runner.SpawnAsync(prefab, pos, Quaternion.identity, player);

            var model = obj.GetComponent<PlayerNetworkModel>();
            var staticData = _dataProvider.PlayerConfig;

            var playerData = new NetworkPlayerStaticData
            {
                Nickname = nickName,
                Color = color,
                Speed = staticData.MoveSpeed,
                Cooldown = staticData.ShootCooldown
            };
            
            model.SetNetworkData(playerData);

            return model;    
        }

        public async UniTask<EnemyNetworkModel> CreateEnemy(Vector2 pos)
        {
            var prefab = await _assetProvider.LoadAndGetComponent<EnemyNetworkModel>(EnemyAssetPath);
            var obj = await _runner.SpawnAsync(prefab, position: pos);

            var model = obj.GetComponent<EnemyNetworkModel>();
            model.Construct(_dataProvider.EnemyConfig);

            return model;
        }

        public async UniTask<UIRoot> CreateUIRoot()
        {
            var prefab = await _assetProvider.LoadAndGetComponent<UIRoot>(RootUIAssetPath);
            var root = Object.Instantiate(prefab, _sceneDependenciesProvider.UIRoot, true);

            return _uiRoot = root;
        }    
        
        public async UniTask<GameStartView> CreateGameStartView()
        {
            var prefab = await _assetProvider.LoadAndGetComponent<GameStartView>(GameStartUIAssetPath);
            var view = Object.Instantiate(prefab);
            
            var rect = view.GetComponent<RectTransform>();
            rect.SetParent(_uiRoot.Self, false);
            
            return view;
        }

        public async UniTask<PlayerDeathView> CreatePlayerDeathView()
        {
            var prefab = await _assetProvider.LoadAndGetComponent<PlayerDeathView>(DeathUIAssetPath);
            var view = Object.Instantiate(prefab);
            
            var rect = view.GetComponent<RectTransform>();
            rect.SetParent(_uiRoot.Self, false);
            
            return view;
        }
        
        public async UniTask<GameResultsView> CreateGameResultsView()
        {
            var prefab = await _assetProvider.LoadAndGetComponent<GameResultsView>(GameResultsUIAssetPath);
            var view = Object.Instantiate(prefab);
            
            var rect = view.GetComponent<RectTransform>();
            rect.SetParent(_uiRoot.Self, false);
            
            return view;
        }
        
        public async UniTask<PlayerUIView> CreatePlayerUIView()
        {
            var prefab = await _assetProvider.LoadAndGetComponent<PlayerUIView>(PlayerUIAssetPath);
            var obj = Object.Instantiate(prefab);

            var view = obj.GetComponent<PlayerUIView>();
            var rect = view.GetComponent<RectTransform>();
            rect.SetParent(_uiRoot.PlayerViewsContainer);

            return view;
        }

        public async UniTask<ProjectileNetworkedModel> CreateProjectile(Vector2 pos, PlayerRef owner)
        {
            var prefab = await _assetProvider.LoadAndGetComponent<ProjectileNetworkedModel>(ProjectileAssetPath);
            var obj = await _runner.SpawnAsync(prefab, position: pos, inputAuthority: owner);

            var model = obj.GetComponent<ProjectileNetworkedModel>();
            model.Construct(_dataProvider.ProjectileConfig, owner);

            return model;
        }

        public async UniTask<LevelModel> CreateLevel()
        {
            var prefab = await _assetProvider.LoadAndGetComponent<LevelModel>(LevelAssetPath);
            var obj = await _runner.SpawnAsync(prefab);

            var model = obj.GetComponent<LevelModel>();

            return model;
        }
    }
}