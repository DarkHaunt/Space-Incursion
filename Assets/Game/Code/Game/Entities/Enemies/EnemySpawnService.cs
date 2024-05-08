using Fusion;
using Game.Code.Game.Level.BoxArea;
using Game.Code.Game.StaticData;
using VContainer.Unity;

namespace Game.Code.Game.Services
{
    public class EnemySpawnService : IInitializable
    {
        private readonly GameStaticDataProvider _staticDataProvider;
        private readonly GameFactory _gameFactory;
        
        private BoxPointsArea _levelArea;

        public EnemySpawnService(GameFactory gameFactory, GameStaticDataProvider staticDataProvider)
        {
            _staticDataProvider = staticDataProvider;
            _gameFactory = gameFactory;
        }

        public void Initialize()
        {
            
        }

        public void SetUpSpawnArea(BoxPointsArea levelArea)
        {
            _levelArea = levelArea;
        }

        public void Tick(NetworkRunner runner, float deltaTime)
        {
            
        }
    }
}