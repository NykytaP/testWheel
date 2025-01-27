using System;

namespace Core.Data.ViewData
{
    public class WheelPopupViewData
    {
        public WheelPopupViewData(Action onSpinClicked)
        {
            OnSpinClicked = onSpinClicked;
        }

        public Action OnSpinClicked { get; }
    }
}