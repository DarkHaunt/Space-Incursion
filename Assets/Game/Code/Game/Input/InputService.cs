using ExitGames.Client.Photon.StructWrapping;
using Game.Code.Extensions;
using UnityEngine;

namespace Game.Code.Game
{
    public class InputService
    {
        private static readonly Vector2 CameraCenterViewport = Vector2.one * 0.5f; 
        
        private readonly Camera _camera;

        public InputService(Camera camera)
        {
            _camera = camera;
        }

        public PlayerInputData GetPlayerInput()
        {
            var data = new PlayerInputData();
            
            data.Buttons.Set(PlayerButtons.Shoot, IsShootButtonPressed());
            data.ShootDirection = GetShootDirection();
            data.MoveDirection = GetMoveDirection();
            
            //Debug.Log($"<color=white>Log input - {data.ShootDirection} | {data.MoveDirection} - {data.Buttons}</color>");
            
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
            var screenPos = _camera.ViewportToWorldPoint(position: CameraCenterViewport);
            var mousePos = Input.mousePosition;

            return Vector2Extensions.Direction(from: screenPos, to: mousePos);
        }

        private bool IsShootButtonPressed() =>
            Input.GetKeyDown(key: KeyCode.Mouse0);
    }
}