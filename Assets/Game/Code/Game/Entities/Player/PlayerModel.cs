using Game.Scripts.Infrastructure.TickManaging;
using System;
using Fusion;
using Game.Code.Game.Services;
using Game.Code.Game.Shooting;
using Game.Code.Game.StaticData.Player;
using UnityEngine;

namespace Game.Code.Game.Entities
{
    public class PlayerModel : NetworkBehaviour, ITickListener
    {
        public event Action<ITickListener> OnDisposed;

        [SerializeField] private ShootModule _shoot;
        [SerializeField] private PhysicMove _move;

        [Networked] private NetworkButtons ButtonsPrevious { get; set; }

        
        public void Construct(PlayerConfig config, GameFactory gameFactory)
        {
            _move.Construct(config.MoveSpeed);
            _shoot.Construct(gameFactory);
        }

        public void Tick(float deltaTime)
        {
            /*if (GetInput(out PlayerInputData input))
            {
                _move.RotateToFace(input.ShootDirection);
                _move.Move(input.MoveDirection, deltaTime);

                if (input.Buttons.WasPressed(ButtonsPrevious, PlayerButtons.Shoot))
                    _shoot.Shoot(Runner);

                ButtonsPrevious = input.Buttons;
            }*/
        }

        public override void Despawned(NetworkRunner runner, bool hasState) =>
            Disable();

        private void Disable() =>
            OnDisposed?.Invoke(this);
    }
}