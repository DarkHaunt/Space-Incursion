using UnityEngine;
using Fusion;
using TMPro;

namespace Game.Code.Game.UI
{
    public class PlayerUIView : NetworkBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _nickText;

        
        public void UpdateScore(int score) =>
            _scoreText.text = score.ToString();

        public void UpdateNickname(string nickName) =>
            _nickText.text = $"{nickName}:";

        public void UpdateTextColor(Color color)
        {
            Debug.Log($"<color=white>Color - {color}</color>");
            _nickText.color = color;
            _scoreText.color = color;
        }
    }
}