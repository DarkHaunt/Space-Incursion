using Game.Code.Game.StaticData.Indents;
using Fusion;

namespace Game.Code.Game
{
    public class NetworkStartArgsProvider
    {
        public StartGameArgs GameArgs { get; private set; }

        public void SetStartArgs(string roomName)
        {
            GameArgs = new StartGameArgs
            {
                PlayerCount = GameIndents.PlayerCount,
                GameMode = GameMode.AutoHostOrClient,
                SessionName = roomName,
            };
        }
    }
}