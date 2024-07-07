using Game.Code.Game.StaticData.Indents;
using VContainer.Unity;

namespace Game.Code.Menu.MVP
{
    public class MenuModel : IInitializable
    {
        public string PlayerName { get; private set; }
        public string RoomName { get; private set; }

        public void SetRoomName(string name) =>
            RoomName = name;
        public void SetPlayerName(string name) =>
            PlayerName = name;

        public void Initialize()
        {
            PlayerName = NetworkIndents.DefaultPlayerName;
            RoomName = NetworkIndents.DefaultRoomName;
        }
    }
}