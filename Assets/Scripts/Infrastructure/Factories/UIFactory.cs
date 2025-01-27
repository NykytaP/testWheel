using System.Threading;
using System.Threading.Tasks;
using Core.Loaders.MainUI;
using Cysharp.Threading.Tasks;
using Infrastructure.Helpers.GameObjectHelper;
using Infrastructure.Helpers.UISceneHelper;
using UnityEngine;

namespace Infrastructure.Factories
{
    public class UIFactory : IUIFactory, IUISceneHelper
    {
        private readonly IMainUILoader _mainUILoader;
        private readonly IGameObjectHelper _gameObjectHelper;

        private Canvas _uiRoot;

        public UIFactory(IMainUILoader mainUILoader, IGameObjectHelper gameObjectHelper)
        {
            _mainUILoader = mainUILoader;
            _gameObjectHelper = gameObjectHelper;
        }

        public async Task InitUIRoot()
        {
            Canvas prefab = await _mainUILoader.LoadUIRoot();
            _uiRoot = _gameObjectHelper.InstantiateObjectWithComponentInScene<Canvas>(prefab.gameObject);
            _uiRoot.transform.SetParent(null);
        }

        public void CleanUp()
        {
            _mainUILoader.Dispose();
        }

        public async UniTask<Transform> GetUIRoot()
        {
            if (!_uiRoot)
            {
                await InitUIRoot();
            }

            return _uiRoot.transform;
        }
        
        public CancellationToken GetSceneCancellationToken()
        {
            return RootGameObjectInstance.GetCancellationTokenOnDestroy();
        }

        public GameObject RootGameObjectInstance => _uiRoot.gameObject;
    }
}