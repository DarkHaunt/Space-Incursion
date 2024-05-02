using Game.Scripts.Infrastructure.TickManaging;
using UnityEngine;
using System;
using Fusion;
using Game.Code.Game.StaticData;

namespace Game.Code.Game.Projectiles
{
    public class ProjectileModel : NetworkBehaviour, ITickListener
    {
        public event Action<ITickListener> OnDisposed;

        [SerializeField] private PlayerProjectileBehavior _behavior;
        [SerializeField] private PhysicMove _move;
        
        private Vector2 _direction;


        public void Construct(ProjectileConfig projectileConfig)
        {
            _move.Construct(projectileConfig.Speed);
            _behavior.Construct();
        }

        public void Tick(float deltaTime) =>
            _move.Move(_direction, deltaTime);

        public ProjectileModel SetMoveDirection(Vector2 dir)
        {
            _direction = dir;
            return this;
        }

        public void Dispose()
        {
            OnDisposed?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}