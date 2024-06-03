using Game.Code.Game.StaticData;
using UnityEngine;
using Fusion;

namespace Game.Code.Game.Projectiles
{
    public class ProjectileNetworkedModel : NetworkBehaviour
    {
        [SerializeField] private PlayerProjectileBehavior _behavior;
        [SerializeField] private PhysicMove _move;

        [Networked] private TickTimer Lifetime { get; set; }


        public void Construct(ProjectileConfig projectileConfig)
        {
            _behavior.Construct();
            
            _move.SetMoveSpeed(projectileConfig.Speed);
            Lifetime = TickTimer.CreateFromSeconds(Runner, projectileConfig.Lifetime);
        }

        public void SetupAndMove(Vector2 moveDirection)
        {
            _move.MoveWithVelocity(moveDirection);
            _move.RotateToFace(moveDirection);
        }

        public override void FixedUpdateNetwork()
        {
            if (Lifetime.Expired(Runner))
                Dispose();
        }

        private void Dispose()
        {
            _behavior.Dispose();
            Runner.Despawn(Object);
        }
    }
}