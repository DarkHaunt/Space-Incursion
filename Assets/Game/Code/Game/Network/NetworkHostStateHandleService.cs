using Game.Code.Game.Services;
using Cysharp.Threading.Tasks;
using Fusion;

namespace Game.Code.Game
{
    public class NetworkHostStateHandleService
    {
        private readonly NetworkRunner _runner;
        
        private bool _isHostInitializing;

        public bool IsHost
            => _runner.CanSpawn;
        
        public NetworkHostStateHandleService(NetworkMonoServiceLocator serviceLocator) =>
            _runner = serviceLocator.Runner;

        public void SetHostIsInitialized() =>
            _isHostInitializing = false;

        public UniTask WaitUntilHostInitialized() =>
            UniTask.WaitWhile(() => IsHost && _isHostInitializing);
    }
}