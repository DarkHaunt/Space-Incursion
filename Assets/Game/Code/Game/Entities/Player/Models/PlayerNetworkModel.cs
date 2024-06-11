using System;
using Game.Code.Game.Services;
using Game.Code.Game.Shooting;
using UnityEngine;
using Fusion;

namespace Game.Code.Game.Entities
{
    [ScriptHelp(BackColor = ScriptHeaderBackColor.Olive)]
    public class PlayerNetworkModel : NetworkBehaviour
    {
        [SerializeField] private PlayerGraphic _graphic;
        [SerializeField] private ShootModule _shoot;
        [SerializeField] private PhysicMove _move;

        private PlayerHandleService _handleService;
        private ChangeDetector _changeDetector;

        [Networked] public NetworkPlayerStaticData Data { get; set; }
        [Networked] public int Score { get; set; }
        [Networked] private NetworkButtons ButtonsPrevious { get; set; }


        public void Construct(PlayerHandleService handleService, GameFactory gameFactory)
        {
            _handleService = handleService;
            _shoot.Construct(gameFactory);

            UpdateNetworkDependentData();
        }

        public void SetNetworkData(NetworkPlayerStaticData staticData) =>
            Data = staticData;

        public void IncreaseScore() =>
            Score++;

        private void UpdateNetworkDependentData()
        {
            _handleService.UpdatePlayerScoreView(Object.InputAuthority, Score);
            _move.SetMoveSpeed(Data.Speed);
        }

        public override void Spawned() =>
            _changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);

        public override void FixedUpdateNetwork()
        {
            if (Runner.TryGetInputForPlayer(Object.InputAuthority, out PlayerInputData input))
            {
                _move.RotateToFace(input.ShootDirection);
                _move.MoveWithVelocity(input.MoveDirection);

                if (input.Buttons.WasPressed(ButtonsPrevious, PlayerButtons.Shoot) && HasStateAuthority)
                    _shoot.Shoot(Object.InputAuthority);

                ButtonsPrevious = input.Buttons;
            }
        }

        public override void Render()
        {
            foreach (var change in _changeDetector.DetectChanges(this, out var previousBuffer, out var currentBuffer))
            {
                switch (change)
                {
                    case nameof(Score):
                        _handleService.UpdatePlayerScoreView(Object.InputAuthority, Score);
                        break;
                    case nameof(Data):
                        _move.SetMoveSpeed(Data.Speed);
                        break;
                }
            }
        }
    }
}