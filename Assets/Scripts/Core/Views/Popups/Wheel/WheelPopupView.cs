using Core.Data.ViewData;
using Core.PopupSystem.Views;
using UnityEngine;

namespace Core.Views.Popups.Wheel
{
    public class WheelPopupView : BasePopupView
    {
        [SerializeField]
        private WheelView _wheelView;

        private WheelPopupViewData _cachedViewData;
        
        public override void SetData(object data)
        {
            if (data is WheelPopupViewData viewData)
            {
                _cachedViewData = viewData;
                _wheelView.SpinButton.onClick.AddListener(() => _cachedViewData.OnSpinClicked?.Invoke());
            }
        }
    }
}