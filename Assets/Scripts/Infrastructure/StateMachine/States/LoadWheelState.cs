using System.Threading.Tasks;
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
            
            await InitUIRoot();
            await SpawnWheel();

            await StartGameLoop();
        }

        private async UniTask SpawnWheel()
        {
            IWheelPopupViewPresenter presenter = _container.Resolve<IWheelPopupViewPresenter>();

            await presenter.ShowPopup(_cancellationTokenHelper.GetSceneCancellationToken());
        }

        private async UniTask InitUIRoot()
        {
            IUIFactory uiFactory = _container.Resolve<IUIFactory>();
            
            await uiFactory.InitUIRoot();
        }

        private async Task StartGameLoop()
        {
            await _stateMachine.Enter<GameLoopState>();
        }
    }
}