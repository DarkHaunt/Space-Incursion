using VContainer.Unity;
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Fusion;
using Game.Code.Game.Services;
using Game.Code.Game.StaticData;
using Game.Code.Infrastructure.SceneManaging;
using UnityEngine;

namespace Game.Code.Game.Boot
{
    public class GameBootstrapper : IAsyncStartable, IDisposable
    {
        private readonly GameStaticDataProvider _dataProvider;
        private readonly TransitionHandler _transitionHandler;
        private readonly NetworkService _networkService;
        private readonly NetworkRunner _networkRunner;


        public GameBootstrapper(NetworkServiceLocator networkServiceLocator, TransitionHandler transitionHandler, 
            GameStaticDataProvider dataProvider, NetworkService networkService)
        {
            _transitionHandler = transitionHandler;
            _networkService = networkService;
            _dataProvider = dataProvider;
            
            _networkRunner = networkServiceLocator.Runner;
        }


        public async UniTask StartAsync(CancellationToken cancellation)
        {
            _networkRunner.ProvideInput = true;
            _networkRunner.AddCallbacks
            (
                _networkService
            );

            var args = new StartGameArgs
            {
                GameMode = Application.isEditor ? GameMode.Host : GameMode.Client,
                SessionName = "test"
            };

            await _dataProvider.PrewarmData();
            
            await _networkRunner.StartGame(args);
            await _transitionHandler.PlayFadeOutAnimation();
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