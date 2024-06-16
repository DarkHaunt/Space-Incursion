using UnityEngine;

namespace Game.Code.Game.UI
{
    public class UIRoot : MonoBehaviour
    {
        [field: SerializeField] public RectTransform Self { get; private set; }
        [field: SerializeField] public RectTransform PlayerViewsContainer { get; private set; }
    }
}