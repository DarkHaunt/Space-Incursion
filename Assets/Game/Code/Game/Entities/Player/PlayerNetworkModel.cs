using Fusion;
using Game.Code.Game.Services;
using Game.Code.Game.Shooting;
using Game.Code.Game.StaticData.Player;
using UnityEngine;

namespace Game.Code.Game.Entities
{
    public class PlayerNetworkModel : NetworkBehaviour
    {
        [SerializeField] private PlayerGraphic _graphic;
        [SerializeField] private ShootModule _shoot;
        [SerializeField] private PhysicMove _move;
        
        private ChangeDetector _changeDetector;

        [Networked] private NetworkButtons ButtonsPrevious { get; set; }
        [Networked] private NetworkString<_16> NickName { get; set; }
        [Networked] private int Score { get; set; }


        public void Construct(PlayerConfig config, GameFactory gameFactory)
        {
            _move.Construct(config.MoveSpeed);
            _shoot.Construct(gameFactory);
        }

        public void SetColor(Color color) =>
            _graphic.SetColor(color);

        public override void Spawned() =>
            _changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);

        public override void FixedUpdateNetwork()
        {
            if (!Object.HasStateAuthority)
                return;

            if (GetInput(out PlayerInputData input))
            {
                _move.RotateToFace(input.ShootDirection);
                _move.Move(input.MoveDirection, Runner.DeltaTime);

                if (input.Buttons.WasPressed(ButtonsPrevious, PlayerButtons.Shoot))
                {
                    Debug.Log($"<color=white>Shoot</color>");
                    _shoot.Shoot(Runner);
                }

                ButtonsPrevious = input.Buttons;
            }
        }

        public override void Render()
        {
            foreach (var change in _changeDetector.DetectChanges(this, out var previousBuffer, out var currentBuffer))
            {
                switch (change)
                {
                    case nameof(NickName):
                        break;
                    case nameof(Score):
                        break;
                }
            }
        }
    }
}