using Core.Data.ViewData;
using Core.PopupSystem.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Views.Popups.StartGame
{
    public class StartGamePopupView : BasePopupView
    {
        [SerializeField]
        private Button _playButton;
        
        public override void SetData(object data)
        {
            if (data is StartGamePopupViewData viewData)
            {
                _playButton.onClick.AddListener(() => viewData.OnPlayClick?.Invoke());
            }
        }
    }
}