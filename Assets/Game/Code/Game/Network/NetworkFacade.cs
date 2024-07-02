using Game.Code.Infrastructure.SceneManaging;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Code.Game.Input;
using Fusion.Sockets;
using Fusion;
using System;

namespace Game.Code.Game.Network
{
    public class NetworkFacade : INetworkRunnerCallbacks
    {
        private readonly NetworkPlayerHandleService _networkPlayerHandleService;
        private readonly InputService _inputService;
        private readonly SceneLoader _sceneLoader;


        public NetworkFacade(InputService inputService, SceneLoader sceneLoader, NetworkPlayerHandleService networkPlayerHandleService)
        {
            _networkPlayerHandleService = networkPlayerHandleService;
            _inputService = inputService;
            _sceneLoader = sceneLoader;
        }


        public void OnInput(NetworkRunner runner, NetworkInput input) =>
            input.Set(_inputService.GetPlayerInput());

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) =>
            _networkPlayerHandleService.AddPlayerToSpawnQueue(player);

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) =>
            _networkPlayerHandleService.DespawnPlayer(player);

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
            _networkPlayerHandleService.DespawnAllPlayers();
            _sceneLoader.Load(Scenes.Menu).Forget();
        }

        #region [Unimplemented Callbacks]

        public void OnConnectedToServer(NetworkRunner runner)
        {
        }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
        {
        }

        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {
        }

        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {
        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        {
        }

        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
        {
        }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {
        }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
        {
        }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        {
        }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
        {
        }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        {
        }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
        {
        }

        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
        {
        }

        public void OnSceneLoadDone(NetworkRunner runner)
        {
        }

        public void OnSceneLoadStart(NetworkRunner runner)
        {
        }

        #endregion
    }
}