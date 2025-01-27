using System;

namespace Core.Data.ViewData
{
    public class CharacterSelectorPopupViewData
    {
        public CharacterSelectorPopupViewData(Action onPlayClick, Action onGenerateNewCharfacterClick)
        {
            OnPlayClick = onPlayClick;
            OnGenerateNewCharfacterClick = onGenerateNewCharfacterClick;
        }

        public Action OnPlayClick { get; }
        public Action OnGenerateNewCharfacterClick { get; }
    }
}