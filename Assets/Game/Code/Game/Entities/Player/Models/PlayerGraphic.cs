using Cysharp.Threading.Tasks;
using Game.Code.Game.Services.Models;
using UnityEngine;

namespace Game.Code.Game.Entities
{
    public class PlayerGraphic : MonoBehaviour
    {
        [SerializeField] private EntityGraphic _entityGraphic;
        
        public UniTask PlayDeathParticle() =>
            _entityGraphic.PlayDestroyGraphics();
    }
}