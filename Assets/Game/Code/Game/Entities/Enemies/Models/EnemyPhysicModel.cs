using Game.Code.Common.Physic;
using Game.Code.Game.Entities;
using UnityEngine;

namespace Game.Code.Game.Services
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