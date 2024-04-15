using Code.Infrastructure.AssetManaging;
using Game.Code.Game.Entities;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Fusion;

using static Game.Code.Game.StaticData.Indents.AddressableLabels;

namespace Game.Code.Game.Services
{
    public class GameFactory
    {
        private readonly NetworkTickService _tickService;
        private readonly AssetProvider _assetProvider;
        private readonly NetworkRunner _networkRunner;

        public GameFactory(AssetProvider assetProvider, NetworkRunner networkRunner, NetworkTickService tickService)
        {
            _assetProvider = assetProvider;
            _networkRunner = networkRunner;
            _tickService = tickService;
        }
        
        public async UniTask<PlayerModel> CreatePlayer(Vector2 pos)
        {
            var prefab = await _assetProvider.LoadAndGetComponent<PlayerModel>(PlayerLabel);
            var obj = await _networkRunner.SpawnAsync(prefab, position: pos);

            var model = obj.GetComponent<PlayerModel>();
            
            _tickService.AddListener(model);
            model.OnDeactivate += () => _tickService.RemoveListener(prefab);
            
            return model;
        }        
        
        public async UniTask<EnemyModel> CreateEnemy(Vector2 pos)
        {
            var prefab = await _assetProvider.LoadAndGetComponent<EnemyModel>(EnemyLabel);
            var obj = await _networkRunner.SpawnAsync(prefab, position: pos);
            
            var model = obj.GetComponent<EnemyModel>();
            
            _tickService.AddListener(model);
            model.OnDeactivate += () => _tickService.RemoveListener(prefab);
            
            return model;
        }
    }
}