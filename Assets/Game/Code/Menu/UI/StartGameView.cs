using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Menu.UI
{
    public class StartGameView : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        
        [field: Space]
        [field: SerializeField] public Button StartButton { get; private set; }
        [field: SerializeField] public Button CancelButton { get; private set; }
        
        
        public void Enable(bool enable)
            => _canvas.enabled = enable;
    }
}