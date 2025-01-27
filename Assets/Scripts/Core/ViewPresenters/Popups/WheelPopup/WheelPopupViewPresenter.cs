using System.Threading;
using Core.Data.ViewData;
using Core.Loaders.Popups.Wheel;
using Core.Views.Popups.Wheel;
using Cysharp.Threading.Tasks;
using Infrastructure.Factories;
using Infrastructure.Helpers.GameObjectHelper;
using Infrastructure.Helpers.ViewPresenter;
using Infrastructure.StateMachine;
using UnityEngine;

namespace Core.ViewPresenters.Popups.WheelPopup
{
    public class WheelPopupViewPresenter : ViewPresenterBase, IWheelPopupViewPresenter
    {
        private readonly IWheelPopupViewLoader _loader;
        private readonly IUIFactory _uiFactory;
        private readonly IGameObjectHelper _gameObjectHelper;
        private readonly GameStateMachine _gameStateMachine;

        public WheelPopupViewPresenter(IWheelPopupViewLoader loader, IUIFactory uiFactory, IGameObjectHelper gameObjectHelper, GameStateMachine gameStateMachine)   
        {
            _loader = loader;
            _uiFactory = uiFactory;
            _gameObjectHelper = gameObjectHelper;
            _gameStateMachine = gameStateMachine;
        }
        
        public async UniTask ShowPopup(CancellationToken cancellationToken)
        {
            AddToDisposables(_loader.Dispose);

            CancellationTokenSource viewTokenSource = new CancellationTokenSource();
            RegisterToken(cancellationToken, viewTokenSource.Token);
            
            UniTask<WheelPopupView> popupPrefabTask = _loader.LoadPopupView(cancellationToken);
            UniTask<Transform> uiRootTask = _uiFactory.GetUIRoot();
            
            var (popupPrefab, uiRoot) = await UniTask.WhenAll(popupPrefabTask, uiRootTask);
            
            if (_cachedCancellationToken.IsCancellationRequested)
                return;

            WheelPopupView mainView = _gameObjectHelper.InstantiateObjectWithComponentInScene<WheelPopupView>(popupPrefab.gameObject, uiRoot);

            mainView.OnClose += () => viewTokenSource.Cancel();
            mainView.SetData(GetViewData());
        }

        private WheelPopupViewData GetViewData()
        {
            return new WheelPopupViewData(HandleSpinClicked);
        }

        private void HandleSpinClicked()
        {
            Debug.LogError("Spin");
        }
    }
}