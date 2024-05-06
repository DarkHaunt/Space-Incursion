using System;
using Code.Infrastructure.UpdateRunner;
using Game.Code.Extensions;
using Game.Scripts.Infrastructure.TickManaging;
using UnityEngine;
using VContainer.Unity;

namespace Game.Code.Game
{
    public class InputService : ITickListener, IStartable, IDisposable
    {
        private static readonly Vector2 CameraCenterViewport = Vector2.one * 0.5f;

        private readonly ITickSource _tickSource;
        private readonly Camera _camera;

        private bool _pressedShootButton;
        private Vector2 _shootDirection;
        private Vector2 _moveDirection;

        public InputService(Camera camera, ITickSource tickSource)
        {
            _camera = camera;
            _tickSource = tickSource;
        }

        public void Tick(float deltaTime) =>
            CollectInput(deltaTime);

        private void CollectInput(float deltaTime)
        {
            _pressedShootButton = IsShootButtonPressed();
            
            _shootDirection = GetShootDirection();
            _moveDirection = GetMoveDirection();
        }

        private void ClearInput()
        {
            _pressedShootButton = false;
        }

        public PlayerInputData GetPlayerInput()
        {
            var data = new PlayerInputData();

            data.Buttons.Set(PlayerButtons.Shoot, _pressedShootButton);
            data.ShootDirection = _shootDirection;
            data.MoveDirection = _moveDirection;

            ClearInput();

            return data;
        }

        private Vector2 GetMoveDirection()
        {
            var horizontalMovement = Input.GetAxis("Horizontal");
            var verticalMovement = Input.GetAxis("Vertical");

            return new Vector2(horizontalMovement, verticalMovement).normalized;
        }

        private Vector2 GetShootDirection()
        {
            var screenPos = _camera.ViewportToScreenPoint(position: CameraCenterViewport);
            var mousePos = Input.mousePosition;

            return Vector2Extensions.Direction(from: screenPos, to: mousePos);
        }

        private bool IsShootButtonPressed() =>
            _pressedShootButton || Input.GetButtonDown("Fire1");

        
        public void Start() =>
            _tickSource.AddListener(this);
        public void Dispose() =>
            _tickSource.RemoveListener(this);
    }
}