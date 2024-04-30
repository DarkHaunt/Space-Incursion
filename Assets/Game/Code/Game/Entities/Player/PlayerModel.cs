using Game.Scripts.Infrastructure.TickManaging;
using System;
using Fusion;
using Game.Code.Game.StaticData.Player;
using UnityEngine;

namespace Game.Code.Game.Entities
{
    public class PlayerModel : NetworkBehaviour, ITickListener
    {
        public event Action<ITickListener> OnDisposed;

        [SerializeField] private PhysicNetworkMove _move;


        public void Construct(PlayerConfig config)
        {
            _move.Construct(config.MoveSpeed);
        }

        public void Tick(float deltaTime)
        {
            if (GetInput(out PlayerInputData input))
            {
                _move.RotateToFace(input.ShootDirection);
                _move.Move(input.MoveDirection, deltaTime);
            }
        }

        public override void Despawned(NetworkRunner runner, bool hasState) =>
            Disable();

        private void Disable() =>
            OnDisposed?.Invoke(this);
    }
}