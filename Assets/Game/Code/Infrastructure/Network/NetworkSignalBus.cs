using System;
using Fusion;

namespace Game.Code.Infrastructure.Network
{
    public class NetworkSignalBus : SimulationBehaviour
    {
        public static event Action OnGameStarted;
        
        [Rpc]
        public static void RPC_GameStarted(NetworkRunner _) =>
            OnGameStarted?.Invoke();
    }
}