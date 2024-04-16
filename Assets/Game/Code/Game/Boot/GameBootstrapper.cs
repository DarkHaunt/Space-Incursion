using VContainer.Unity;
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using Fusion;
using UnityEngine;

namespace Game.Code.Game.Boot
{
    public class GameBootstrapper : IAsyncStartable, IDisposable
    {
        private readonly NetworkService _networkService;
        private readonly NetworkRunner _networkRunner;


        public GameBootstrapper(NetworkRunner networkRunner, NetworkService networkService)
        {
            _networkRunner = networkRunner;
            _networkService = networkService;
        }


        public async UniTask StartAsync(CancellationToken cancellation)
        {
            _networkRunner.ProvideInput = true;
            _networkRunner.AddCallbacks
            (
                _networkService
            );

            var args = new StartGameArgs
            {
                GameMode = Application.isEditor ? GameMode.Client : GameMode.Host,
                SessionName = "test"
            };

            await _networkRunner.StartGame(args);
        }

        public void Dispose()
        {
            _networkRunner.RemoveCallbacks
            (
                _networkService
            );
        }
    }
}