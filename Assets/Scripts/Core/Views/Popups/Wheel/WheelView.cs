using UnityEngine;
using UnityEngine.UI;

namespace Core.Views.Popups.Wheel
{
    public class WheelView : MonoBehaviour
    {
        [SerializeField]
        private Button _spinButton;

        public Button SpinButton => _spinButton;
    }
}