using Fusion;
using UnityEngine;

namespace Game.Code.Game.Entities.Player.Data
{
    public struct NetworkPlayerStaticData : INetworkStruct
    {
        public NetworkString<_16> Nickname;
        public float Cooldown;
        public float Speed;
        public Color Color;
    }
}