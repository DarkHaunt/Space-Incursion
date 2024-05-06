using Fusion;
using Game.Code.Game.Services;
using Game.Code.Game.Shooting;
using Game.Code.Game.StaticData.Player;
using UnityEngine;

namespace Game.Code.Game.Entities
{
    public class PlayerNetworkModel : NetworkBehaviour
    {
        [SerializeField] private ShootModule _shoot;
        [SerializeField] private PhysicMove _move;

        [Networked] private NetworkButtons ButtonsPrevious { get; set; }

        
        public void Construct(PlayerConfig config, GameFactory gameFactory)
        {
            _move.Construct(config.MoveSpeed);
            _shoot.Construct(gameFactory);
        }

        public override void FixedUpdateNetwork()
        {
            if(!Object.HasStateAuthority)
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
    }
}