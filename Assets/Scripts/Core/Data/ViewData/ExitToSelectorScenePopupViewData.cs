using System;

namespace Core.Data.ViewData
{
    public class ExitToSelectorScenePopupViewData
    {
        public ExitToSelectorScenePopupViewData(Action onExitClick)
        {
            OnExitClick = onExitClick;
        }

        public Action OnExitClick { get; }
    }
}