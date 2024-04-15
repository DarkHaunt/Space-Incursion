using System;
using Fusion;
using Game.Scripts.Infrastructure.TickManaging;

namespace Game.Code.Game.Services
{
    public class EnemyModel : NetworkBehaviour, ITickListener
    {
        public event Action OnDeactivate;
        
        public void Tick(float deltaTime)
        {
            
        }

        public override void Despawned(NetworkRunner runner, bool hasState) =>
            Disable();

        private void Disable() =>
            OnDeactivate?.Invoke();
    }
}