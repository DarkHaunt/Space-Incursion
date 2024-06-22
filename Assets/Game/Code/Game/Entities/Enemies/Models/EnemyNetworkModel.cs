using Game.Code.Game.StaticData.Scriptables;
using Game.Code.Game.Services.Models;
using Game.Code.Extensions;
using UnityEngine;
using DG.Tweening;
using System;
using Fusion;

namespace Game.Code.Game.Services
{
    [ScriptHelp(BackColor = ScriptHeaderBackColor.Olive)]
    public class EnemyNetworkModel : NetworkBehaviour
    {
        public event Action<PlayerRef> OnKilledBy;
        
        [Header("--- Models ---")]
        [SerializeField] private EnemyPhysicModel _physicModel;
        [SerializeField] private EntityGraphic _graphic;
        [SerializeField] private PhysicMove _move;


        public override void Spawned() =>
            _graphic.PlayFireParticle(true);

        public void Construct(EnemyConfig config)
        {
            _move.SetMoveSpeed(config.MoveSpeed);
            _physicModel.Construct();
        }

        public void StartMoveTo(Vector2 point)
        {
            var direction = Vector2Extensions.Direction(_move.Position, point);
            
            _move.RotateToFace(direction);
            _move.MoveWithTween(point)
                .SetEase(Ease.Linear)
                .OnComplete(Despawn);
        }

        public async void Kill(PlayerRef killer)
        {
            OnKilledBy?.Invoke(killer);
            
            _physicModel.EnableCollider(false);
            _move.Stop();
            
            RPC_DeathGraphicEffect();

            await _graphic.WaitUntilDeathEffectEnds();

            Despawn();
        }

        [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.All)]
        private void RPC_DeathGraphicEffect()
        {
            _graphic.PlayDestroyGraphics();
            _graphic.PlayFireParticle(false);
        }

        private void Despawn() =>
            Runner.Despawn(Object);
    }
}