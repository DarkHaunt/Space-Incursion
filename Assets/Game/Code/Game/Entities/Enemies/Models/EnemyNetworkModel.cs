using Game.Code.Game.StaticData.Scriptables;
using Game.Code.Game.Services.Models;
using Game.Code.Extensions;
using UnityEngine;
using DG.Tweening;
using Fusion;

namespace Game.Code.Game.Services
{
    public class EnemyNetworkModel : NetworkBehaviour
    {
        [SerializeField] private Collider2D _collider2D;

        [Header("--- Models ---")]
        [SerializeField] private EntityGraphic _graphic;
        [SerializeField] private PhysicMove _move;
        
        
        public void Construct(EnemyConfig config) =>
            _move.SetMoveSpeed(config.MoveSpeed);

        public void StartMoveTo(Vector2 point)
        {
            var direction = Vector2Extensions.Direction(_move.Position, point);
            
            _move.RotateToFace(direction);
            _move.MoveWithTween(point)
                .SetEase(Ease.Linear)
                .OnComplete(Despawn);
        }

        public async void Kill()
        {
            _collider2D.enabled = false;
            _move.Stop();

            await _graphic.PlayDestroyGraphics();

            Despawn();
        }

        private void Despawn() =>
            Runner.Despawn(Object);
    }
}