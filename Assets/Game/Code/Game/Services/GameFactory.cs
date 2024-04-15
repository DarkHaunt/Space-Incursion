using Code.Infrastructure.AssetManaging;
using Game.Code.Game.Entities;
using Cysharp.Threading.Tasks;
using Game.Code.Extensions;
using UnityEngine;

using static Game.Code.Game.StaticData.Indents.AddressableLabels;

namespace Game.Code.Game.Services
{
    public class GameFactory
    {
        private readonly AssetProvider _assetProvider;

        public GameFactory(AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public async UniTask<PlayerModel> CreatePlayer(Vector2 pos)
        {
            var view = await _assetProvider.LoadAndGetComponent<PlayerModel>(PlayerLabel);
            view.transform.SetInPosition(pos);
            
            return view;
        }        
        
        public async UniTask<EnemyModel> CreateEnemy(Vector2 pos)
        {
            var view = await _assetProvider.LoadAndGetComponent<EnemyModel>(EnemyLabel);
            view.transform.SetInPosition(pos);
            
            return view;
        }
    }
}