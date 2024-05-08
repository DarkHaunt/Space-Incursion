using Game.Code.Common.Physic;
using Game.Code.Game.Services;
using UnityEngine;

namespace Game.Code.Game.Projectiles
{
    [RequireComponent(typeof(TriggerObserver))]
    public class PlayerProjectileBehavior : MonoBehaviour
    {
        private TriggerObserver _observer;
        
        public void Construct()
        {
            _observer = GetComponent<TriggerObserver>();
            _observer.OnTriggerEnter += HandleTriggerEnter;
        }

        public void Dispose() =>
            _observer.OnTriggerEnter -= HandleTriggerEnter;

        private void HandleTriggerEnter(Collider2D obj)
        {
            if (obj.TryGetComponent(out EnemyNetworkModel enemyModel))
            {
                // TODO: LOgic
            }
        }
    }
}