using Game.Code.Game.StaticData.Scriptables;
using Code.Infrastructure.AssetManaging;
using Game.Code.Game.StaticData.Player;
using Cysharp.Threading.Tasks;
using static Game.Code.Game.StaticData.Indents.AddressableLabels;

namespace Game.Code.Game.StaticData
{
    public class GameStaticDataProvider
    {
        private readonly AssetProvider _assetProvider;

        public ProjectileConfig ProjectileConfig { private set; get; }
        public PlayerConfig PlayerConfig { get; private set; }
        public EnemyConfig EnemyConfig { get; private set; }
        public GameConfig GameConfig { get; private set; }


        public GameStaticDataProvider(AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public async UniTask PrewarmData()
        {
            var tasks = new[]
            {
                LoadProjectileConfig(),
                LoadPlayerConfig(),
                LoadEnemyConfig(),
                LoadGameConfig(),
            };

            await UniTask.WhenAll(tasks);
        }

        private async UniTask LoadProjectileConfig() =>
            ProjectileConfig = await _assetProvider.Load<ProjectileConfig>(ProjectileConfigLabel);

        private async UniTask LoadPlayerConfig() =>
            PlayerConfig = await _assetProvider.Load<PlayerConfig>(PlayerConfigLabel);

        private async UniTask LoadEnemyConfig() =>
            EnemyConfig = await _assetProvider.Load<EnemyConfig>(EnemyConfigLabel);

        private async UniTask LoadGameConfig() =>
            GameConfig = await _assetProvider.Load<GameConfig>(GameConfigLabel);
    }
}