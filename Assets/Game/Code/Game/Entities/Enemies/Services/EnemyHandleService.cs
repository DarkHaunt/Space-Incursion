using System;
using System.Collections;
using Game.Code.Common.CoroutineRunner;
using Game.Code.Extensions;
using Game.Code.Game.Level;
using Game.Code.Game.StaticData.Scriptables;
using Game.Code.Game.Level.BoxArea;
using Game.Code.Game.StaticData;
using UnityEngine;

namespace Game.Code.Game.Services
{
    public class EnemyHandleService : IDisposable
    {
        private readonly EnemySpawnPossibilityProvider _spawnPossibilityProvider;
        private readonly EnemyPositionProvider _positionProvider;
        
        private readonly GameStaticDataProvider _staticDataProvider;
        private readonly NetworkSpawnService _networkSpawnService;
        private readonly PlayerHandleService _playerHandleService;
        private readonly ICoroutineRunner _coroutineRunner;

        private EnemyConfig _enemyConfig;
        private IEnumerator _spawning;
        

        public EnemyHandleService(ICoroutineRunner coroutineRunner, PlayerHandleService playerHandleService,  EnemySpawnPossibilityProvider spawnPossibilityProvider,
            GameStaticDataProvider staticDataProvider, EnemyPositionProvider positionProvider, NetworkSpawnService networkSpawnService)
        {
            _spawnPossibilityProvider = spawnPossibilityProvider;
            _playerHandleService = playerHandleService;
            _networkSpawnService = networkSpawnService;
            _staticDataProvider = staticDataProvider;
            _positionProvider = positionProvider;
            _coroutineRunner = coroutineRunner;
        }

        public void Init(LevelModel levelArea)
        {
            _enemyConfig = _staticDataProvider.EnemyConfig;
            _positionProvider.Init(levelArea);

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
            _positionProvider.CalculateEnemyPositions(out var spawnPos, out var movePos);
            
            var enemy = await _networkSpawnService.CreateEnemy(spawnPos);
            enemy.Construct(_enemyConfig);
            enemy.StartMoveTo(movePos);
            
            enemy.OnKilledBy += _playerHandleService.IncreasePlayerScore;
        }

        public void Dispose()
        {
            _coroutineRunner?.StopRunningCoroutine(_spawning);
            _spawnPossibilityProvider?.Dispose();
        }
    }
}