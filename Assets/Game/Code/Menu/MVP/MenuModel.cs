using Game.Code.Game.StaticData.Indents;
using VContainer.Unity;

namespace Game.Code.Menu.View
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
            PlayerName = GameIndents.DefaultPlayerName;
            RoomName = GameIndents.DefaultRoomName;
        }
    }
}