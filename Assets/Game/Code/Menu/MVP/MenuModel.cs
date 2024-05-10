namespace Game.Code.Menu.View
{
    public class MenuModel
    {
        public string PlayerName { get; private set; }
        public string RoomName { get; private set; }

        public void SetRoomName(string name) =>
            RoomName = name;
        public void SetPlayerName(string name) =>
            PlayerName = name;
    }
}