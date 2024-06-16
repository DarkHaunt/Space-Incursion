using System;

namespace Game.Code.Game.StaticData.Indents
{
    public static class NetworkIndents
    {
        public static readonly TimeSpan ClientObjectSearchTimeout = TimeSpan.FromSeconds(5f);
        
        public const int MaxPlayersCount = 4;
        public const int MinPlayersCount = 2;
        
        public const string DefaultPlayerName = "Player";
        public const string DefaultRoomName = "Room";
    }
}