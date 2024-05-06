using Code.Infrastructure.AssetManaging;
using Game.Code.Game.Projectiles;
using Game.Code.Game.StaticData;
using Game.Code.Game.Entities;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Fusion;
using Game.Scripts.Infrastructure.PoolComponent;
using static Game.Code.Game.StaticData.Indents.AddressableLabels;

namespace Game.Code.Game.Services
{
    public class GameFactory
    {
        private readonly GameStaticDataProvider _dataProvider;
        private readonly NetworkTickService _tickService;
        private readonly AssetProvider _assetProvider;

        private readonly PoolComponent<ProjectileModel> _projectilePool;

        
        public GameFactory(AssetProvider assetProvider, GameStaticDataProvider dataProvider, NetworkServiceLocator networkServiceLocator)
        {
            _assetProvider = assetProvider;
            _dataProvider = dataProvider;
            
            _tickService = networkServiceLocator.TickService;
        }
        
        
        public async UniTask<PlayerNetworkModel> CreatePlayer(NetworkRunner runner, Vector2 pos, PlayerRef player)
        {
            var prefab = await _assetProvider.LoadAndGetComponent<PlayerNetworkModel>(PlayerLabel);
            var obj = await runner.SpawnAsync(prefab, pos, Quaternion.identity, player);
            
            var model = obj.GetComponent<PlayerNetworkModel>();
            model.Construct(_dataProvider.PlayerConfig, this);
            
            return model;
        }        
        
        public async UniTask<EnemyModel> CreateEnemy(NetworkRunner runner, Vector2 pos)
        {
            var prefab = await _assetProvider.LoadAndGetComponent<EnemyModel>(EnemyLabel);
            var obj = await runner.SpawnAsync(prefab, position: pos);

            var model = obj.GetComponent<EnemyModel>();
            
            return model;
        }

        public async UniTask<ProjectileModel> CreateProjectile(NetworkRunner runner, Vector2 pos)
        {
            var prefab = await _assetProvider.LoadAndGetComponent<ProjectileModel>(ProjectileLabel);
            var obj = await runner.SpawnAsync(prefab, position: pos);

            var model = obj.GetComponent<ProjectileModel>();
            model.Construct(_dataProvider.ProjectileConfig);
            
            return model;
        }
    }
}