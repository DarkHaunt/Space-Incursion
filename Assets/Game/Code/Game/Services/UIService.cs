using Cysharp.Threading.Tasks;
using Game.Code.Game.UI;
using UnityEngine;

namespace Game.Code.Game.Services
{
    public class UIService : MonoBehaviour
    {
        [SerializeField] private float _deathScreenAppearTime;
        [SerializeField] private float _resultsScreenAppearTime;

        private PlayerDeathView _playerDeathView;
        private GameResultsView _gameResultsView;
        
        public void Init(PlayerDeathView playerDeathView, GameResultsView gameResultsView)
        {
            _playerDeathView = playerDeathView;
            _gameResultsView = gameResultsView;

            _playerDeathView.Hide();
            _gameResultsView.Hide();
        }
        
        public void ShowDeathScreen() =>
            _playerDeathView.Show(_deathScreenAppearTime).Forget();

        public void ShowGameOverPanel() =>
            _gameResultsView.Show(_resultsScreenAppearTime).Forget();
    }
}