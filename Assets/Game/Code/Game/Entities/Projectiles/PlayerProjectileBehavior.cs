using Game.Code.Common.Physic;
using Game.Code.Game.Services;
using UnityEngine;
using System;
using Fusion;

namespace Game.Code.Game.Projectiles
{
    [RequireComponent(typeof(TriggerObserver))]
    public class PlayerProjectileBehavior : MonoBehaviour
    {
        public event Action OnEnemyHit;
        
        private TriggerObserver _observer;
        private PlayerRef _ownerRef;

        public void Construct(PlayerRef ownerRef)
        {
            _ownerRef = ownerRef;
            
            _observer = GetComponent<TriggerObserver>();
            _observer.OnTriggerEnter += HandleTriggerEnter;
        }

        private void HandleTriggerEnter(Collider2D obj)
        {
            if (obj.TryGetComponent(out EnemyNetworkModel enemy))
            {
                enemy.Kill(_ownerRef);
                OnEnemyHit?.Invoke();
            }
        }
    }
}