using VContainer.Unity;
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Fusion;
using Game.Code.Game.Services;
using Game.Code.Game.StaticData;

namespace Game.Code.Game.Boot
{
    public class GameBootstrapper : IAsyncStartable, IDisposable
    {
        private readonly GameStaticDataProvider _dataProvider;
        private readonly NetworkService _networkService;
        private readonly NetworkRunner _networkRunner;


        public GameBootstrapper(NetworkServiceLocator networkServiceLocator,
            GameStaticDataProvider dataProvider, NetworkService networkService)
        {
            _networkService = networkService;
            _dataProvider = dataProvider;

            _networkRunner = networkServiceLocator.Runner;
        }


        public async UniTask StartAsync(CancellationToken cancellation)
        {
            _networkRunner.AddCallbacks
            (
                _networkService
            );

            await _dataProvider.PrewarmData();
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