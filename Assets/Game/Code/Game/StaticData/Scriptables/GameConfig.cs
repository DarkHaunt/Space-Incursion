using UnityEngine;

namespace Game.Code.Game.StaticData.Scriptables
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Scriptables/GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [field: Header("--- Enemies ---")]
        [field: SerializeField] public AnimationCurve EnemiesSpawnIncome { get; private set; }
        [field: SerializeField] public int EnemiesSpawnPickCount { get; private set; }
        [field: SerializeField] public float EnemiesSpawnIncomeTime { get; private set; }
    }
}