using System.Threading.Tasks;
using Core.Services.DataManager;
using Core.ViewPresenters.Popups.BalancePanelViewPresenter;
using Core.ViewPresenters.Popups.WheelPopup;
using Cysharp.Threading.Tasks;
using Infrastructure.Data;
using Infrastructure.Factories;
using Infrastructure.Helpers.CancellationTokenHelper;
using Infrastructure.SceneManagement;
using UnityEngine;
using Zenject;

namespace Infrastructure.StateMachine.States
{
    public class LoadWheelState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        private DiContainer _container;
        private ICancellationTokenHelper _cancellationTokenHelper;

        public LoadWheelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public async Task Enter()
        {
            await _sceneLoader.Load(SceneName.WheelScene, OnLoaded);
        }

        public async Task Exit()
        {
        }

        private async void OnLoaded(SceneName sceneName)
        {
            _container = Object.FindObjectOfType<SceneContext>().Container;
            _cancellationTokenHelper = _container.Resolve<ICancellationTokenHelper>();

            LoadData();
            await InitUIRoot();
            await SpawnWheel();
            await InitUI();

            await StartGameLoop();
        }

        private void LoadData()
        {
            IDataManager dataManager = _container.Resolve<IDataManager>();
            
            dataManager.LoadBalanceData();
        }

        private async UniTask InitUIRoot()
        {
            IUIFactory uiFactory = _container.Resolve<IUIFactory>();
            
            await uiFactory.InitUIRoot();
        }
        
        private async UniTask SpawnWheel()
        {
            IWheelPopupViewPresenter presenter = _container.Resolve<IWheelPopupViewPresenter>();

            await presenter.ShowPopup(_cancellationTokenHelper.GetSceneCancellationToken());
        }
        
        private async UniTask InitUI()
        {
            IBalancePanelViewPresenter balancePanelViewPresenter = _container.Resolve<IBalancePanelViewPresenter>();

            await balancePanelViewPresenter.ShowPanel(_cancellationTokenHelper.GetSceneCancellationToken());
        }

        private async Task StartGameLoop()
        {
            await _stateMachine.Enter<GameLoopState>();
        }
    }
}