using Fusion;
using Game.Code.Game.Level.BoxArea;
using UnityEngine;

namespace Game.Code.Game.Level
{
    public class LevelModel : NetworkBehaviour
    {
        [field: SerializeField] public BoxPointsArea ArenaArea { get; private set; }
    }
}