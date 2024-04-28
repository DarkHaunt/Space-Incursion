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

        [SerializeField] private PhysicMove _move;


        public void Construct(PlayerConfig config)
        {
            _move.Construct(config.MoveSpeed);
        }

        public void Tick(float deltaTime)
        {
            Debug.Log($"<color=white>Tick</color>");
            
            if (GetInput(out PlayerInputData input))
            {
                Debug.Log($"<color=white>Input provide</color>");

                _move.RotateToFace(input.ShootDirection);
                _move.Move(input.MoveDirection);
            }
        }

        public override void Despawned(NetworkRunner runner, bool hasState) =>
            Disable();

        private void Disable() =>
            OnDisposed?.Invoke(this);
    }
}