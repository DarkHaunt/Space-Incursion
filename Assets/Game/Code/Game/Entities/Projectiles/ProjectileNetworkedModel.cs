using Fusion;
using Game.Code.Game.Entities.Shared.Models;
using Game.Code.Game.StaticData.Scriptables;
using UnityEngine;

namespace Game.Code.Game.Entities.Projectiles
{
    [ScriptHelp(BackColor = ScriptHeaderBackColor.Olive)]
    public class ProjectileNetworkedModel : NetworkBehaviour
    {
        [SerializeField] private PlayerProjectileBehavior _behavior;
        [SerializeField] private PhysicMove _move;

        [Networked] private TickTimer Lifetime { get; set; }


        public void Construct(ProjectileConfig projectileConfig, PlayerRef ownerRef)
        {
            _behavior.Construct(ownerRef);
            _behavior.OnEnemyHit += Dispose;
            
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
        
        private void Dispose() =>
            Runner.Despawn(Object);
    }
}