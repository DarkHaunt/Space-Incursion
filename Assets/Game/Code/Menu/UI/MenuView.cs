using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Menu.UI
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        
        [field: SerializeField] public Button StartButton { get; private set; }
        [field: SerializeField] public Button ExitButton { get; private set; }


        public void Enable(bool enable)
            => _canvas.enabled = enable;
    }
}