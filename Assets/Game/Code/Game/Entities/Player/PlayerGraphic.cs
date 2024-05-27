using TMPro;
using UnityEngine;

namespace Game.Code.Game.Entities
{
    public class PlayerGraphic : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private TextMeshProUGUI _nickText;


        public void SetColor(Color color) =>
            _spriteRenderer.color = color;
    }
}