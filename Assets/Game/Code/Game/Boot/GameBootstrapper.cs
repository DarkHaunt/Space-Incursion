using VContainer.Unity;
using System;
using Fusion;

namespace Game.Code.Game.Boot
{
    public class GameBootstrapper : IInitializable, IDisposable
    {
        private readonly NetworkService _networkService;
        private readonly NetworkRunner _networkRunner;
        

        public GameBootstrapper(NetworkRunner networkRunner, NetworkService networkService)
        {
            _networkRunner = networkRunner;
            _networkService = networkService;
        }


        public void Initialize()
        {
            _networkRunner.AddCallbacks
            (
                _networkService
            );
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