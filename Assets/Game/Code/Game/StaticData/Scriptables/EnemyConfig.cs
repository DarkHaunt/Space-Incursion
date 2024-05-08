using UnityEngine;

namespace Game.Code.Game.StaticData.Scriptables
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Scriptables/Enemy", order = 0)]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public float MoveSpeed { get; private set; }
    }
}