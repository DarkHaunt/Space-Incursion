using Code.Infrastructure.AssetManaging;
using Game.Code.Game.Projectiles;
using Game.Code.Game.StaticData;
using Game.Code.Game.Entities;
using Cysharp.Threading.Tasks;
using Game.Code.Game.Level;
using UnityEngine;
using Fusion;
using Game.Code.Game.UI;
using static Game.Code.Game.StaticData.Indents.AddressableIndents;

namespace Game.Code.Game.Services
{
    public class GameFactory
    {
        private readonly GameStaticDataProvider _dataProvider;
        private readonly AssetProvider _assetProvider;
        
        private readonly NetworkRunner _runner;


        public GameFactory(AssetProvider assetProvider, GameStaticDataProvider dataProvider, NetworkMonoServiceLocator networkServiceLocator)
        {
            _assetProvider = assetProvider;
            _dataProvider = dataProvider;
            
            _runner = networkServiceLocator.Runner;
        }
        
        
        public async UniTask<PlayerNetworkModel> CreatePlayer(Vector2 pos, PlayerRef player)
        {
            var prefab = await _assetProvider.LoadAndGetComponent<PlayerNetworkModel>(PlayerAssetPath);
            var obj = await _runner.SpawnAsync(prefab, pos, Quaternion.identity, player);
            
            var model = obj.GetComponent<PlayerNetworkModel>();
            model.Construct(_dataProvider.PlayerConfig, this);
            
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

        public async UniTask<PlayerUIView> CreatePlayerUI(RectTransform parent)
        {
            var prefab = await _assetProvider.LoadAndGetComponent<PlayerUIView>(PlayerUIAssetPath);
            var obj = await _runner.SpawnAsync(prefab);

            var view = obj.GetComponent<PlayerUIView>();
            var rect = view.GetComponent<RectTransform>();
            rect.SetParent(parent);

            return view;
        }

        public async UniTask<UIRoot> CreateUIRoot(Transform parent)
        {
            var prefab = await _assetProvider.LoadAndGetComponent<UIRoot>(RootUIAssetPath);
            var root = Object.Instantiate(prefab, parent, true);

            return root;
        }

        public async UniTask<ProjectileModel> CreateProjectile(Vector2 pos)
        {
            var prefab = await _assetProvider.LoadAndGetComponent<ProjectileModel>(ProjectileAssetPath);
            var obj = await _runner.SpawnAsync(prefab, position: pos);

            var model = obj.GetComponent<ProjectileModel>();
            model.Construct(_dataProvider.ProjectileConfig);
            
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