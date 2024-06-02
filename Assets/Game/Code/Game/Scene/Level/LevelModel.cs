using System.Collections.Generic;
using UnityEngine;

namespace Game.Code.Game.Level
{
    public class LevelModel : MonoBehaviour
    {
        [field: SerializeField] public Transform LeftBottomEnemySpawnPoint { get; private set; } 
        [field: SerializeField] public Transform RightTopEnemySpawnPoint { get; private set; }
    }
}