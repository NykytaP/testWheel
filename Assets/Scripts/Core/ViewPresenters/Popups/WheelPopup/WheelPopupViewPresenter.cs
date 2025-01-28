using System.Threading;
using Core.Data;
using Core.Data.Entities;
using Core.Data.ViewData;
using Core.Loaders.Popups.Wheel;
using Core.Services.BalanceService;
using Core.Services.PrizeGenerator;
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
        private readonly IPrizeGenerator _prizeGenerator;
        private readonly IBalanceService _balanceService;
        private readonly SignalBus _signalBus;

        private PrizeEntity[] _cachedPrizes;
        private WheelPopupView _mainView;
        
        public WheelPopupViewPresenter(IWheelPopupViewLoader loader, IUIFactory uiFactory, IGameObjectHelper gameObjectHelper, 
            IPrizeGenerator prizeGenerator, IBalanceService balanceService, SignalBus signalBus)   
        {
            _loader = loader;
            _uiFactory = uiFactory;
            _gameObjectHelper = gameObjectHelper;
            _prizeGenerator = prizeGenerator;
            _balanceService = balanceService;
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

            _mainView = _gameObjectHelper.InstantiateObjectWithComponentInScene<WheelPopupView>(popupPrefab.gameObject, uiRoot);

            _mainView.OnClose += () => viewTokenSource.Cancel();
            _mainView.SetData(GetViewData());
        }

        private WheelPopupViewData GetViewData()
        {
            _cachedPrizes = _prizeGenerator.GenerateAndCachePrizes(Constants.WheelOfFortuneCore.PrizesAmount);
            
            return new WheelPopupViewData(HandleSpinClicked, _cachedPrizes);
        }

        private async void HandleSpinClicked()
        {
            int prizeIndex = GetRandomPrize();
            PrizeEntity prizeEntity = _cachedPrizes[prizeIndex];

            _balanceService.AddMoney(prizeEntity.Amount);
            
            await _mainView.RotateWheel(prizeIndex);
            
            _signalBus.Fire<BalanceUpdatedSignal>();
            Debug.LogError($"Balance updated: {_balanceService.GetBalance()}");
        }

        private int GetRandomPrize()
        {
            if (_cachedPrizes.Length == 0)
                return -1;

            return Random.Range(0, _cachedPrizes.Length);
        }
    }
}