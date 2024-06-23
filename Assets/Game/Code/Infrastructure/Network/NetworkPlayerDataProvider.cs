using Fusion;
using Game.Code.Game.StaticData.Indents;

namespace Game.Code.Infrastructure.Network
{
    public class NetworkPlayerDataProvider
    {
        public PlayerData PlayerData { get; private set; }
        
        public void SetPlayerData(string roomName, string nickName)
        {
            PlayerData = new PlayerData
            {
                GameArgs = new StartGameArgs
                {
                    PlayerCount = NetworkIndents.MaxPlayersCount,
                    GameMode = GameMode.AutoHostOrClient,
                    SessionName = roomName,
                },

                Nickname = nickName
            };
        }
    }

    public record PlayerData
    {
        public StartGameArgs GameArgs;
        public string Nickname;
    }
}