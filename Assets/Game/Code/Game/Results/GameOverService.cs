using Game.Code.Game.Entities.Player.Services;
using VContainer.Unity;
using System;
using UniRx;
using UnityEngine;

namespace Game.Code.Game.Services
{
    public class GameOverService : IInitializable, IDisposable
    {
        public event Action<GameResultsData> OnGameOver;
        
        private readonly CompositeDisposable _disposables = new();
        private readonly PlayerHandleService _playerHandleService;

        
        public GameOverService(PlayerHandleService playerHandleService)
        {
            _playerHandleService = playerHandleService;
        }


        public void Initialize()
        {
            _playerHandleService.OnPlayerKilled
                .Subscribe(_ => HandlePlayerDead())
                .AddTo(_disposables);
        }

        private void HandlePlayerDead()
        {
            if(_playerHandleService.AlivePlayersCount == 0)
                NotifyGameOver();
        }

        private void NotifyGameOver()
        {
            var scores = _playerHandleService.GetAllPlayersScores();
            var data = new GameResultsData(scores);

            OnGameOver?.Invoke(data);
        }

        public void Dispose() =>
            _disposables?.Dispose();
    }
}