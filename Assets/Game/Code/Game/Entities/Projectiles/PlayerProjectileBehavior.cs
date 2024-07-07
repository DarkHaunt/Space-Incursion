using System;
using Fusion;
using Game.Code.Game.Entities.Enemies.Models;
using Game.Code.Infrastructure.Physic;
using UnityEngine;

namespace Game.Code.Game.Entities.Projectiles
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