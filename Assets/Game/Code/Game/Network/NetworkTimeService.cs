using Fusion;

namespace Game.Code.Game.Services
{
    public class NetworkTimeService 
    {
        private readonly NetworkRunner _runner;

        public float DeltaTime => _runner.DeltaTime;

        public NetworkTimeService(NetworkRunner runner)
        {
            _runner = runner;
        }
    }
}