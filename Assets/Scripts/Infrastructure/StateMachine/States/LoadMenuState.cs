using System.Threading.Tasks;
using Core.ViewPresenters.Popups.StartGamePopup;
using Cysharp.Threading.Tasks;
using Infrastructure.Data;
using Infrastructure.Factories;
using Infrastructure.Helpers.CancellationTokenHelper;
using Infrastructure.SceneManagement;
using UnityEngine;
using Zenject;

namespace Infrastructure.StateMachine.States
{
    public class LoadMenuState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        private DiContainer _container;
        private ICancellationTokenHelper _cancellationTokenHelper;

        public LoadMenuState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public async Task Enter()
        {
            await _sceneLoader.Load(SceneName.MenuScene, OnLoaded);
        }

        public async Task Exit()
        {
        }

        private async void OnLoaded(SceneName sceneName)
        {
            _container = Object.FindObjectOfType<SceneContext>().Container;
            _cancellationTokenHelper = _container.Resolve<ICancellationTokenHelper>();
            
            await InitUIRoot();
            await ShowStartGamePopup();

            await _stateMachine.Enter<GameLoopState>();
        }

        private async UniTask ShowStartGamePopup()
        {
            IStartGamePopupViewPresenter presenter = _container.Resolve<IStartGamePopupViewPresenter>();

            await presenter.ShowPopup(_cancellationTokenHelper.GetSceneCancellationToken());
        }

        private async Task InitUIRoot()
        {
            IUIFactory uiFactory = _container.Resolve<IUIFactory>();
            
            await uiFactory.InitUIRoot();
        }
    }
}