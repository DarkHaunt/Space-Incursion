using UnityEngine;

namespace Game.Code.Game.StaticData.Scriptables
{
    [CreateAssetMenu(fileName = "ProjectileConfig", menuName = "Scriptables/Projectile", order = 1)]
    public class ProjectileConfig : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float Lifetime { get; private set; }
    }
}