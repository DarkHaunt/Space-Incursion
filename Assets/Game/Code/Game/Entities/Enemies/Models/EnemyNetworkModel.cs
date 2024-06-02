using Game.Code.Game.StaticData.Scriptables;
using Game.Code.Game.StaticData.Indents;
using Game.Code.Extensions;
using UnityEngine;
using Fusion;

namespace Game.Code.Game.Services
{
    public class EnemyNetworkModel : NetworkBehaviour
    {
        [SerializeField] private PhysicMove _move;
        
        private Vector2 _movePosition;

        public void Construct(EnemyConfig config) =>
            _move.SetMoveSpeed(config.MoveSpeed);

        public void SetMovePoint(Vector2 point) => 
            _movePosition = point;

        public override void FixedUpdateNetwork()
        {
            var direction = Vector2Extensions.Direction(_movePosition, _move.Position);
            var distance = Vector3.Distance(transform.position, _movePosition);

            if (distance <= GameIndents.EnemyDistanceReachEpsilon)
            {
                HandleReachedPoint();
                return;
            }

            _move.Move(direction, Runner.DeltaTime);
        }

        private void HandleReachedPoint() =>
            Destroy(gameObject);
    }
}