using Game.Code.Game.StaticData.Indents;
using Fusion;

namespace Game.Code.Game
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
                    PlayerCount = NetworkIndents.PlayerCount,
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