using Game.Scripts.Infrastructure.TickManaging;
using System;
using Fusion;

namespace Game.Code.Game.Entities
{
    public class PlayerModel : NetworkBehaviour, ITickListener
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