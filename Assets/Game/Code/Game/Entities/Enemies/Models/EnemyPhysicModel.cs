using Game.Code.Game.Entities.Player.Models;
using Game.Code.Infrastructure.Physic;
using UnityEngine;

namespace Game.Code.Game.Entities.Enemies.Models
{
    [RequireComponent(typeof(TriggerObserver))]
    public class EnemyPhysicModel : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider2D;
        
        private TriggerObserver _observer;
        
        public void Construct()
        {
            _observer = GetComponent<TriggerObserver>();
            _observer.OnTriggerEnter += HandleTriggerEnter;
        }

        public void EnableCollider(bool enable) =>
            _collider2D.enabled = enable;

        private void HandleTriggerEnter(Collider2D obj)
        {
            if (obj.TryGetComponent(out PlayerNetworkModel player))
                player.Kill();
        }
    }
}