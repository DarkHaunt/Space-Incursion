using Fusion;
using UnityEngine;

namespace Game.Code.Infrastructure.Network
{
    public class NetworkMonoServiceLocator : MonoBehaviour
    {
        [field: SerializeField] public NetworkRunner Runner { get; private set; }
        [field: SerializeField] public NetworkSignalBus SignalBus { get; private set; }
    }
}