using System;

namespace Game.Code.Game.StaticData.Indents
{
    public static class NetworkIndents
    {
        public static readonly TimeSpan ClientObjectSearchTimeout = TimeSpan.FromSeconds(5f);
        public const int PlayerCount = 4;
        public const string DefaultPlayerName = "Player";
        public const string DefaultRoomName = "Room";
    }
}