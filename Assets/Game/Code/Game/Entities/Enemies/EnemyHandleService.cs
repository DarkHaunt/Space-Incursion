using System;
using System.Collections;
using Game.Code.Common.CoroutineRunner;
using Game.Code.Extensions;
using Game.Code.Game.StaticData.Scriptables;
using Game.Code.Game.Level.BoxArea;
using Game.Code.Game.StaticData;
using UnityEngine;

namespace Game.Code.Game.Services
{
    public class EnemyHandleService : IDisposable
    {
        private readonly EnemySpawnPossibilityProvider _spawnPossibilityProvider;
        private readonly GameStaticDataProvider _staticDataProvider;
        private readonly NetworkSpawnService _networkSpawnService;
        private readonly ICoroutineRunner _coroutineRunner;

        private BoxPointsArea _levelArea;
        private EnemyConfig _enemyConfig;
        
        private IEnumerator _spawning;

        public EnemyHandleService(ICoroutineRunner coroutineRunner, EnemySpawnPossibilityProvider spawnPossibilityProvider, 
            GameStaticDataProvider staticDataProvider, NetworkSpawnService networkSpawnService)
        {
            _spawnPossibilityProvider = spawnPossibilityProvider;
            _networkSpawnService = networkSpawnService;
            _staticDataProvider = staticDataProvider;
            _coroutineRunner = coroutineRunner;
        }

        public void Init(BoxPointsArea levelArea)
        {
            _enemyConfig = _staticDataProvider.EnemyConfig;
            _levelArea = levelArea;

            _spawning = SpawnProcess();
            _coroutineRunner.RunCoroutine(_spawning);
        }

        private IEnumerator SpawnProcess()
        {
            while (true)
            {
                yield return Yields.GetWaitForEndOfFrame();

                _spawnPossibilityProvider.Tick(Time.deltaTime);

                if (_spawnPossibilityProvider.CanSpawn())
                {
                    _spawnPossibilityProvider.Reload();
                    SpawnEnemy();
                }
            }
        }

        private async void SpawnEnemy()
        {
            var enemy = await _networkSpawnService.CreateEnemy(Vector2.zero);
            
            enemy.Construct(_enemyConfig);
            enemy.SetMovePoint(_levelArea.GetRandomPositionInsideExclude());
        }

        public void Dispose()
        {
            _coroutineRunner?.StopRunningCoroutine(_spawning);
            _spawnPossibilityProvider?.Dispose();
        }
    }
}