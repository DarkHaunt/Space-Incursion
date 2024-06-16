using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace Game.Code.Game.UI
{
    public class GameStartView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _playerCount;
        [field: SerializeField] public Button StartButton { get; private set; }
        
        public void SetPlayersCount(int count, int maxCount) =>
            _playerCount.text = $"{count}/{maxCount}";

        public void EnableStartButton(bool enable) =>
            StartButton.interactable = enable;
    }
}