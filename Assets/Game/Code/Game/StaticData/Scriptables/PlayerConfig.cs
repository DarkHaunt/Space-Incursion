using Fusion;
using UnityEngine;

namespace Game.Code.Game.StaticData.Player
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Scriptables/Player", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float ShootCooldown { get; private set; }
    }
}