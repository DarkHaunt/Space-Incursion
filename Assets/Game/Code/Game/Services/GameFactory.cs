using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Code.Game.Services
{
    public class GameFactory
    {
        public async UniTask CreatePlayer(Vector2 pos)
        {
            await UniTask.CompletedTask;
        }        
        
        public async UniTask CreateEnemy(Vector2 pos)
        {
            await UniTask.CompletedTask;
        }
    }
}