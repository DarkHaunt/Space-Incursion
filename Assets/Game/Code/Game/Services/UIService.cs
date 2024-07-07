using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Code.Game.UI;
using UnityEngine;
using System;
using UniRx;

namespace Game.Code.Game.Services
{
    public class UIService : MonoBehaviour
    {
        [SerializeField] private float _deathScreenAppearTime;
        [SerializeField] private float _resultsScreenAppearTime;

        private GameDeathView _gameDeathView;
        private GameResultsView _gameResultsView;

        public IObservable<Unit> OnExitButtonClick =>
            Observable.Merge
            (
                _gameResultsView.ExitButton.OnClickAsObservable(),
                _gameDeathView.ExitButton.OnClickAsObservable()
            );

        public void Init(GameDeathView gameDeathView, GameResultsView gameResultsView)
        {
            _gameDeathView = gameDeathView;
            _gameResultsView = gameResultsView;

            _gameDeathView.Hide().Forget();
            _gameResultsView.Hide().Forget();
        }

        public void ShowDeathScreen() =>
            _gameDeathView.Show(_deathScreenAppearTime).Forget();

        public void HideDeathScreen() =>
            _gameDeathView.Hide().Forget();

        public void ShowGameOverPanel(IEnumerable<PlayerResultsView> views)
        {
            _gameResultsView.FillWithResults(views);
            _gameResultsView.Show(_resultsScreenAppearTime).Forget();
        }
    }
}