using System;
using Game.Code.Extensions;
using Game.Code.Game.Scene;
using Game.Code.Infrastructure.TickManaging;
using UnityEngine;
using VContainer.Unity;

namespace Game.Code.Game.Input
{
    public class InputService : ITickListener, IStartable, IDisposable
    {
        private static readonly Vector2 CameraCenterViewport = Vector2.one * 0.5f;

        private readonly SceneDependenciesProvider _dependenciesProvider;
        private readonly ITickSource _tickSource;
        private Camera _camera;

        private bool _pressedShootButton;
        private Vector2 _shootDirection;
        private Vector2 _moveDirection;

        public InputService(SceneDependenciesProvider dependenciesProvider, ITickSource tickSource)
        {
            _dependenciesProvider = dependenciesProvider;
            _tickSource = tickSource;
        }

        public void Tick(float deltaTime) =>
            CollectInput();

        private void CollectInput()
        {
            _pressedShootButton = IsShootButtonPressed();
            
            _shootDirection = GetShootDirection();
            _moveDirection = GetMoveDirection();
        }

        private void ClearInput() =>
            _pressedShootButton = false;

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
            var horizontalMovement = UnityEngine.Input.GetAxis("Horizontal");
            var verticalMovement = UnityEngine.Input.GetAxis("Vertical");

            return new Vector2(horizontalMovement, verticalMovement).normalized;
        }

        private Vector2 GetShootDirection()
        {
            var screenPos = _camera.ViewportToScreenPoint(position: CameraCenterViewport);
            var mousePos = UnityEngine.Input.mousePosition;

            return Vector2Extensions.Direction(from: screenPos, to: mousePos);
        }

        private bool IsShootButtonPressed() =>
            _pressedShootButton || UnityEngine.Input.GetButtonDown("Fire1");

        
        public void Start()
        {
            _camera = _dependenciesProvider.MainCamera; 
            _tickSource.AddListener(this);
        }

        public void Dispose() =>
            _tickSource.RemoveListener(this);
    }
}