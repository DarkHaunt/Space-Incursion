using UnityEngine;

namespace Game.Code.Game.StaticData
{
    [CreateAssetMenu(fileName = "ProjectileConfig", menuName = "Scriptables/Projectile", order = 1)]
    public class ProjectileConfig : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
    }
}