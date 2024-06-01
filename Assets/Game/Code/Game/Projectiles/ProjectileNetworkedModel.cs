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

        public void SetupAndMove(Vector2 moveDirection)
        {
            _direction = moveDirection;
        }

        private void Dispose()
        {
            Runner.Despawn(Object);
            Lifetime = TickTimer.None;
            
            Destroy(gameObject); // TODO: Add custom pool implementation
        }
    }
}