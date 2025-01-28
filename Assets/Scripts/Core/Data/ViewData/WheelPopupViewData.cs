using System;
using Core.Data.Entities;

namespace Core.Data.ViewData
{
    public class WheelPopupViewData
    {
        public WheelPopupViewData(Action onSpinClicked, PrizeEntity[] prizeEntities)
        {
            OnSpinClicked = onSpinClicked;
            PrizeEntities = prizeEntities;
        }

        public Action OnSpinClicked { get; }
        public PrizeEntity[] PrizeEntities { get; }
    }
}