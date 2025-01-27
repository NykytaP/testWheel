using System;

namespace Core.Data.ViewData
{
    public class StartGamePopupViewData
    {
        public StartGamePopupViewData(Action onPlayClick)
        {
            OnPlayClick = onPlayClick;
        }

        public Action OnPlayClick { get; }
    }
}