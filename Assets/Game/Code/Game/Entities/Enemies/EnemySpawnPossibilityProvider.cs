using System;
using DG.Tweening;
using Game.Code.Game.StaticData;
using Game.Scripts.Extensions;
using VContainer.Unity;

namespace Game.Code.Game.Services
{
    public class EnemySpawnPossibilityProvider : IInitializable, IDisposable
    {
        private Tweener _timeIncomeTween;

        private readonly GameStaticDataProvider _staticDataProvider;

        private float _spawnCooldown;
        private float _passedTime;

        public EnemySpawnPossibilityProvider(GameStaticDataProvider staticDataProvider)
        {
            _staticDataProvider = staticDataProvider;
        }


        public void Initialize()
        {
            var gameConfig = _staticDataProvider.GameConfig;

            _timeIncomeTween = DOTween.To(
                    getter: () => _spawnCooldown,
                    setter: cooldown => _spawnCooldown = cooldown,
                    endValue: gameConfig.EnemiesSpawnIncomeTime,
                    duration: gameConfig.EnemiesSpawnFullForceTime
                )
                .SetEase(gameConfig.EnemiesSpawnIncome);
        }

        public void Tick(float time) =>
            _passedTime += time;

        public bool CanSpawn() =>
            _passedTime > _spawnCooldown;

        public void Reload() =>
            _passedTime = 0f;

        public void Dispose() =>
            _timeIncomeTween.KillIfValid();
    }
}