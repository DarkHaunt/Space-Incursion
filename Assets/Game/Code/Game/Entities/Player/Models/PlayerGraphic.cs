using UnityEngine;

namespace Game.Code.Game.Entities
{
    public class PlayerGraphic : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        public void SetSprite(Sprite sprite) =>
            _spriteRenderer.sprite = sprite;
    }
}