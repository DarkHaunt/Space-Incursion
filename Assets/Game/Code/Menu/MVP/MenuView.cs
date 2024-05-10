using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Code.Menu.View
{
    public class MenuView : MonoBehaviour
    {
        [field: SerializeField] public Button StartButton { get; private set; }
        [field: SerializeField] public Button ExitButton { get; private set; }
        [field: SerializeField] public TMP_InputField NameField { get; private set; }
        [field: SerializeField] public TMP_InputField RoomField { get; private set; }
    }
}