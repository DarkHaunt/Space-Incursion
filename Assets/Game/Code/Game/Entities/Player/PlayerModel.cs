using Game.Scripts.Infrastructure.TickManaging;
using Unity.Plastic.Antlr3.Runtime.Misc;
using UnityEngine;
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