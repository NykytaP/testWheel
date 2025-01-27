using System.Threading;
using Core.Data.ViewData;
using Core.Loaders.Popups.StartGame;
using Core.Views.Popups.StartGame;
using Cysharp.Threading.Tasks;
using Infrastructure.Factories;
using Infrastructure.Helpers.GameObjectHelper;
using Infrastructure.Helpers.ViewPresenter;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.States;
using UnityEngine;

namespace Core.ViewPresenters.Popups.StartGamePopup
{
    public class StartGamePopupViewPresenter : ViewPresenterBase, IStartGamePopupViewPresenter
    {
        private readonly IStartGamePopupViewLoader _loader;
        private readonly IUIFactory _uiFactory;
        private readonly IGameObjectHelper _gameObjectHelper;
        private readonly GameStateMachine _gameStateMachine;

        public StartGamePopupViewPresenter(IStartGamePopupViewLoader loader, IUIFactory uiFactory, IGameObjectHelper gameObjectHelper, GameStateMachine gameStateMachine)   
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
            
            UniTask<StartGamePopupView> popupPrefabTask = _loader.LoadPopupView(cancellationToken);
            UniTask<Transform> uiRootTask = _uiFactory.GetUIRoot();
            
            var (popupPrefab, uiRoot) = await UniTask.WhenAll(popupPrefabTask, uiRootTask);
            
            if (_cachedCancellationToken.IsCancellationRequested)
                return;

            StartGamePopupView mainView = _gameObjectHelper.InstantiateObjectWithComponentInScene<StartGamePopupView>(popupPrefab.gameObject, uiRoot);

            mainView.OnClose += () => viewTokenSource.Cancel();
            mainView.SetData(GetViewData());
        }

        private StartGamePopupViewData GetViewData()
        {
            return new StartGamePopupViewData(HandleStartGameClicked);
        }

        private void HandleStartGameClicked()
        {
            _gameStateMachine.Enter<LoadWheelState>();
        }
    }
}