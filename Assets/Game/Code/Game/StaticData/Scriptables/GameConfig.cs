using System.Collections.Generic;
using UnityEngine;

namespace Game.Code.Game.StaticData.Scriptables
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Scriptables/GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [field: Header("--- Enemies ---")]
        [field: SerializeField] public AnimationCurve EnemiesSpawnIncome { get; private set; }
        [field: SerializeField] public float EnemiesSpawnIncomeTime { get; private set; }
        [field: SerializeField] public float EnemiesSpawnFullForceTime { get; private set; }
        
        [field: Header("--- Scene ---")]
        [field: SerializeField] public Vector2 CameraLeftBottomBound { get; private set; }
        [field: SerializeField] public Vector2 CameraRightTopBound { get; private set; }

        [field: Header("--- Players ---")]
        [field: SerializeField] public List<Color> AvailableColor { get; private set; }
    }
}