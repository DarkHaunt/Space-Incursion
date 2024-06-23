using Fusion;
using UnityEngine;
using TMPro;

namespace Game.Code.Game.UI
{
    public class PlayerUIView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _nickText;

        
        public void UpdateScore(int score) =>
            _scoreText.text = score.ToString();

        public void UpdateNickname(NetworkString<_16> nickName) =>
            _nickText.text = $"{nickName}:";

        public void UpdateTextColor(Color color)
        {
            _nickText.color = color;
            _scoreText.color = color;
        }
    }
}