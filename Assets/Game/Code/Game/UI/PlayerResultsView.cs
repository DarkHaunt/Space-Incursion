using TMPro;
using UnityEngine;

namespace Game.Code.Game.UI
{
    public class PlayerResultsView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nickText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        
        public int Score { get; private set; }
            

        public void Init(string nick, int score)
        {
            Score = score;
            
            _nickText.text = nick;
            _scoreText.text = score.ToString();
        }
    }
}