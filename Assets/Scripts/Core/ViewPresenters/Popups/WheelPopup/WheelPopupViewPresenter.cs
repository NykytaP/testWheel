using System.Threading;
using Core.Data;
using Core.Data.Entities;
using Core.Data.ViewData;
using Core.Loaders.Popups.Wheel;
using Core.Services.WheelPrizeManager;
using Core.Views.Popups.Wheel;
using Cysharp.Threading.Tasks;
using Infrastructure.Factories;
using Infrastructure.Helpers.GameObjectHelper;
using Infrastructure.Helpers.ViewPresenter;
using Infrastructure.Signals;
using UnityEngine;
using Zenject;

namespace Core.ViewPresenters.Popups.WheelPopup
{
    public class WheelPopupViewPresenter : ViewPresenterBase, IWheelPopupViewPresenter
    {
        private readonly IWheelPopupViewLoader _loader;
        private readonly IUIFactory _uiFactory;
        private readonly IGameObjectHelper _gameObjectHelper;
        private readonly IWheelPrizeManager _wheelPrizeManager;
        private readonly SignalBus _signalBus;

        private PrizeEntity[] _cachedPrizes;
        private WheelPopupView _mainView;
        
        public WheelPopupViewPresenter(IWheelPopupViewLoader loader, IUIFactory uiFactory, IGameObjectHelper gameObjectHelper, IWheelPrizeManager wheelPrizeManager, SignalBus signalBus)   
        {
            _loader = loader;
            _uiFactory = uiFactory;
            _gameObjectHelper = gameObjectHelper;
            _wheelPrizeManager = wheelPrizeManager;
            _signalBus = signalBus;
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

            _cachedPrizes = _wheelPrizeManager.GenerateAndCachePrizes(Constants.WheelOfFortuneCore.PrizesAmount);
            _mainView = _gameObjectHelper.InstantiateObjectWithComponentInScene<WheelPopupView>(popupPrefab.gameObject, uiRoot);

            _mainView.OnClose += () => viewTokenSource.Cancel();
            _mainView.SetData(GetViewData());
        }

        private WheelPopupViewData GetViewData()
        {
            return new WheelPopupViewData(HandleSpinClicked, _cachedPrizes);
        }

        private async void HandleSpinClicked()
        {
            PrizeEntity prize = _wheelPrizeManager.CollectPrize();
            
            await _mainView.RotateWheel(prize.Index);
            
            _signalBus.Fire<BalanceUpdatedSignal>();
        }
    }
}