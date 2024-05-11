using Game.Code.Game.StaticData.Indents;
using System;
using Fusion;

namespace Game.Code.Game
{
    public class NetworkArgsProvider
    {
        public event Action<StartGameArgs> OnGameArgsCreated;

        private StartGameArgs _args;
        
        public void CreateNewGameArgs(string roomName)
        {
            _args = new StartGameArgs
            {
                GameMode = GameMode.AutoHostOrClient,
                PlayerCount = GameIndents.PlayerCount,
                SessionName = roomName,
            };
            
            OnGameArgsCreated?.Invoke(_args);
        }
    }
}