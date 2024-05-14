using Fusion;
using UnityEngine;

namespace Game.Code.Game.Services
{
    public class NetworkMonoServiceLocator : MonoBehaviour
    {
        [field: SerializeField] public NetworkTickService TickService { get; private set; }
        [field: SerializeField] public NetworkRunner Runner { get; private set; }
    }
}