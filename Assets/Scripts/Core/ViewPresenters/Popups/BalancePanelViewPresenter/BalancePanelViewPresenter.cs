using System.Threading;
using Core.Loaders.Popups.BalancePanel;
using Core.Services.BalanceService;
using Core.Views.Popups.BalancePanel;
using Cysharp.Threading.Tasks;
using Infrastructure.Factories;
using Infrastructure.Helpers.GameObjectHelper;
using Infrastructure.Helpers.ViewPresenter;
using Infrastructure.Signals;
using UnityEngine;
using Zenject;

namespace Core.ViewPresenters.Popups.BalancePanelViewPresenter
{
    public class BalancePanelViewPresenter : ViewPresenterBase, IBalancePanelViewPresenter
    {
        private readonly IBalancePanelViewLoader _loader;
        private readonly IUIFactory _uiFactory;
        private readonly IGameObjectHelper _gameObjectHelper;
        private readonly IBalanceService _balanceService;
        private readonly SignalBus _signalBus;

        private BalancePanelView _mainView;

        public BalancePanelViewPresenter(IBalancePanelViewLoader loader, IUIFactory uiFactory, IGameObjectHelper gameObjectHelper, 
            IBalanceService balanceService, SignalBus signalBus)   
        {
            _loader = loader;
            _uiFactory = uiFactory;
            _gameObjectHelper = gameObjectHelper;
            _balanceService = balanceService;
            _signalBus = signalBus;
        }
        
        public async UniTask ShowPanel(CancellationToken cancellationToken)
        {
            AddToDisposables(_loader.Dispose);
            AddToDisposables(UnsubscribeEvents);

            CancellationTokenSource viewTokenSource = new CancellationTokenSource();
            RegisterToken(cancellationToken, viewTokenSource.Token);
            
            UniTask<BalancePanelView> panelPrefabTask = _loader.LoadPanelView(cancellationToken);
            UniTask<Transform> uiRootTask = _uiFactory.GetUIRoot();
            
            var (panelPrefab, uiRoot) = await UniTask.WhenAll(panelPrefabTask, uiRootTask);
            
            if (_cachedCancellationToken.IsCancellationRequested)
                return;

            _mainView = _gameObjectHelper.InstantiateObjectWithComponentInScene<BalancePanelView>(panelPrefab.gameObject, uiRoot);
            
            SubscribeEvents();
            UpdateView();
        }

        private void SubscribeEvents()
        {
            _signalBus.Subscribe<BalanceUpdatedSignal>(UpdateView);
        }

        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<BalanceUpdatedSignal>(UpdateView);
        }

        private void UpdateView()
        {
            _mainView.UpdateBalance(GetBalance());
        }

        private int GetBalance()
        {
            return _balanceService.GetBalance();
        }
    }
}