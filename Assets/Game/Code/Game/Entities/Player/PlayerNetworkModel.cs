using System;
using DG.Tweening;
using Fusion;
using Game.Code.Extensions;
using Game.Code.Game.Services;
using Game.Code.Game.Shooting;
using Game.Code.Game.StaticData.Player;
using UnityEngine;
using VContainer;

namespace Game.Code.Game.Entities
{
    public class PlayerNetworkModel : NetworkBehaviour
    {
        [SerializeField] private PlayerGraphic _graphic;
        [SerializeField] private ShootModule _shoot;
        [SerializeField] private PhysicMove _move;
        [SerializeField] private Rigidbody2D _rigidbody;

        private PlayerHandleService _playerHandleService;

        [Networked] private NetworkButtons ButtonsPrevious { get; set; }
        [Networked] public Color GraphicColor { get; set; }
        [Networked] public string Nickname { get; set; }
        [Networked] private int Score { get; set; }


        public void Construct(PlayerConfig config, GameFactory gameFactory)
        {
            _shoot.Construct(gameFactory);

            UpdateNetworkDependentData();
        }

        [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.InputAuthority | RpcTargets.StateAuthority)]
        public void RPC_NetworkDataSetUp(Color color, string nickName)
        {
            GraphicColor = color;
            Nickname = nickName;
            Score = 0;

            UpdateNetworkDependentData();
        }

        public override void Spawned()
        {
            _move.Construct(3f);

            if (!HasStateAuthority)
                UpdateNetworkDependentData();
        }

        private void UpdateNetworkDependentData()
        {
            _graphic.SetColor(GraphicColor);
        }

        public override void FixedUpdateNetwork()
        {
            if (Runner.TryGetInputForPlayer(Object.InputAuthority, out PlayerInputData input))
            {
                _move.RotateToFace(input.ShootDirection);
                _move.Move(input.MoveDirection, Runner.DeltaTime);

                /*if (input.Buttons.WasPressed(ButtonsPrevious, PlayerButtons.Shoot))
                    _shoot.Shoot(Runner);*/
                
                ButtonsPrevious = input.Buttons;
            }
        }
    }
}