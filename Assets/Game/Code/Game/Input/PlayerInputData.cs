using Fusion;
using UnityEngine;

namespace Game.Code.Game
{
    public struct PlayerInputData : INetworkInput
    {
        public NetworkButtons Buttons;
        
        public Vector2 ShootDirection;
        public Vector2 MoveDirection;
    }
}