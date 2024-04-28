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

        public GameFactory(AssetProvider assetProvider, NetworkTickService tickService)
        {
            _assetProvider = assetProvider;
            _tickService = tickService;
        }
        
        public async UniTask<PlayerModel> CreatePlayer(NetworkRunner runner, Vector2 pos, PlayerRef player)
        {
            var prefab = await _assetProvider.LoadAndGetComponent<PlayerModel>(PlayerLabel);
            var obj = await runner.SpawnAsync(prefab, pos, Quaternion.identity, player);

            var model = obj.GetComponent<PlayerModel>();
            
            _tickService.AddListener(model);
            model.OnDeactivate += () => _tickService.RemoveListener(prefab);
            
            return model;
        }        
        
        public async UniTask<EnemyModel> CreateEnemy(NetworkRunner runner, Vector2 pos)
        {
            var prefab = await _assetProvider.LoadAndGetComponent<EnemyModel>(EnemyLabel);
            var obj = await runner.SpawnAsync(prefab, position: pos);
            
            var model = obj.GetComponent<EnemyModel>();
            
            _tickService.AddListener(model);
            model.OnDeactivate += () => _tickService.RemoveListener(prefab);
            
            return model;
        }
    }
}