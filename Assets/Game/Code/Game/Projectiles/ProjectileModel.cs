using Game.Code.Game.StaticData;
using UnityEngine;
using Fusion;

namespace Game.Code.Game.Projectiles
{
    public class ProjectileModel : NetworkBehaviour
    {
        [SerializeField] private PlayerProjectileBehavior _behavior;
        [SerializeField] private PhysicMove _move;

        [Networked] private TickTimer Lifetime { get; set; }

        private Vector2 _direction;


        public override void FixedUpdateNetwork()
        {
            if(!Object.HasStateAuthority)
                return;
                
            _move.Move(_direction, Runner.DeltaTime);

            if (Lifetime.Expired(Runner))
                Dispose();
        }

        public void Construct(ProjectileConfig projectileConfig)
        {
            _move.Construct(projectileConfig.Speed);
            _behavior.Construct();

            Lifetime = TickTimer.CreateFromSeconds(Runner, projectileConfig.Lifetime);
        }

        public ProjectileModel SetMoveDirection(Vector2 dir)
        {
            _direction = dir;
            return this;
        }

        private void Dispose()
        {
            Destroy(gameObject); // TODO: Add custom pool implementation
            Lifetime = TickTimer.None;
        }
    }
}