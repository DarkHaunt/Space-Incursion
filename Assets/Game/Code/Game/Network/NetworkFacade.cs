using System.Collections.Generic;
using Fusion.Sockets;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using Fusion;
using Game.Code.Game.Services;
using VContainer.Unity;

namespace Game.Code.Game
{
    public class NetworkFacade : INetworkRunnerCallbacks
    {
        private readonly NetworkPlayerDataProvider _dataProvider;
        private readonly GameFactory _gameFactory;

        private readonly PlayerHandleService _playerHandleService;
        private readonly NetworkSpawnService _spawnService;
        private readonly InputService _inputService;


        public NetworkFacade(InputService inputService, PlayerHandleService playerHandleService, NetworkSpawnService spawnService,
            NetworkPlayerDataProvider dataProvider, GameFactory gameFactory)
        {
            _playerHandleService = playerHandleService;
            _inputService = inputService;
            _dataProvider = dataProvider;
            _gameFactory = gameFactory;
            _spawnService = spawnService;
        }


        public void OnInput(NetworkRunner runner, NetworkInput input) =>
            input.Set(_inputService.GetPlayerInput());

        public async void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            if (_playerHandleService.IsPlayerAlreadyRegistered(player))
                return;

            var name = _dataProvider.PlayerData.Nickname;

            var playerModel = await _spawnService.SetUpPlayerData(player, name);
            playerModel.Construct(_gameFactory);
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) =>
            _spawnService.DespawnPlayer(player);

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
            foreach (var player in runner.ActivePlayers)
                _spawnService.DespawnPlayer(player);
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