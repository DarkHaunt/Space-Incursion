using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using Game.Code.Common.CoroutineRunner;
using Game.Code.Extensions;
using Game.Code.Game.Entities.Enemies.Data;
using Game.Code.Game.Level;
using UnityEngine;

namespace Game.Code.Game.Services
{
    public class EnemyHandleService : IDisposable
    {
        private readonly EnemySpawnPossibilityProvider _spawnPossibilityProvider;
        private readonly EnemyPositionProvider _positionProvider;

        private readonly PlayerHandleService _playerHandleService;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly GameFactory _gameFactory;

        private readonly List<EnemyNetworkModel> _existingEnemies = new();
        private readonly NetworkRunner _runner;

        private IEnumerator _spawning;


        public EnemyHandleService(ICoroutineRunner coroutineRunner, PlayerHandleService playerHandleService,
            EnemySpawnPossibilityProvider spawnPossibilityProvider, EnemyPositionProvider positionProvider,
            GameFactory gameFactory, NetworkMonoServiceLocator serviceLocator)
        {
            _spawnPossibilityProvider = spawnPossibilityProvider;
            _playerHandleService = playerHandleService;
            _positionProvider = positionProvider;
            _coroutineRunner = coroutineRunner;
            _gameFactory = gameFactory;

            _runner = serviceLocator.Runner;
        }

        public void Init(LevelModel levelArea)
        {
            _positionProvider.Init(levelArea);
        }

        public void StartSpawning()
        {
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

            var enemy = await _gameFactory.CreateEnemy(spawnPos);
            enemy.StartMoveTo(movePos);

            enemy.OnDeath += HandleEnemyKilled;

            _existingEnemies.Add(enemy);
        }

        private void HandleEnemyKilled(EnemyDeathData enemyDeathData)
        {
            var enemy = enemyDeathData.Self;
            enemy.OnDeath -= HandleEnemyKilled;

            _existingEnemies.Remove(enemy);
            _playerHandleService.IncreasePlayerScore(enemyDeathData.Killer);
        }

        public void Dispose()
        {
            _spawnPossibilityProvider?.Dispose();

            StopSpawning();
            KillAllExistingEnemies();
        }

        public void StopSpawning() =>
            _coroutineRunner?.StopRunningCoroutine(_spawning);

        public void KillAllExistingEnemies() =>
            _existingEnemies.ForEach(x => _runner.Despawn(x.Object));
    }
}