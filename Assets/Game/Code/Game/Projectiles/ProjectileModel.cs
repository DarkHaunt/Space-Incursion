using Game.Scripts.Infrastructure.TickManaging;
using UnityEngine;
using System;
using Fusion;
using Game.Code.Game.StaticData;

namespace Game.Code.Game.Projectiles
{
    public class ProjectileModel : NetworkBehaviour
    {
        [SerializeField] private PlayerProjectileBehavior _behavior;
        [SerializeField] private PhysicMove _move;
        
        private Vector2 _direction;


        public void Construct(ProjectileConfig projectileConfig)
        {
            _move.Construct(projectileConfig.Speed);
            _behavior.Construct();
        }

        public override void FixedUpdateNetwork() =>
            _move.Move(_direction, Runner.DeltaTime);

        public ProjectileModel SetMoveDirection(Vector2 dir)
        {
            _direction = dir;
            return this;
        }

        public void Dispose() =>
            gameObject.SetActive(false);
    }
}