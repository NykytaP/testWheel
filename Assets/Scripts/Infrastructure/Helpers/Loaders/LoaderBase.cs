using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Infrastructure.AssetsManagement;
using UnityEngine;
using Object = UnityEngine.Object;
namespace Infrastructure.Helpers.Loaders
{
    public class LoaderBase : ILoader
    {
        private readonly IAssetProvider _assetProvider;

        private List<object> _cachedAssets;
        private List<string> _cachedGroups;

        protected LoaderBase(IAssetProvider assetsProvider)
        {
            _assetProvider = assetsProvider;

            _cachedGroups = new List<string>();
            _cachedAssets = new List<object>();
        }
        
        protected async UniTask<List<T>> LoadComponentsFromAssets<T>(string groupKey, CancellationToken cancellationToken)
            where T : Object
        {
            List<GameObject> assets = await _assetProvider.LoadGroupAsync<GameObject>(groupKey);
            List<T> components = new List<T>(assets.Count);
            
            _cachedGroups.Add(groupKey);
            
            if (cancellationToken.IsCancellationRequested)
            {
                _assetProvider.ReleaseGroup(groupKey);
                return null;
            }
            
            foreach (GameObject asset in assets)
                components.Add(asset.GetComponent<T>());
            
            return components;
        }

        protected async UniTask<T> LoadAsset<T>(string assetKey, CancellationToken cancellationToken) 
            where T : Object
        {
            var loadedAsset = await _assetProvider.LoadAsync<T>(assetKey);
            
            if (cancellationToken.IsCancellationRequested)
            {
                _assetProvider.ReleaseAsset(loadedAsset);
                return null;
            }

            _cachedAssets.Add(loadedAsset);
            return loadedAsset;
        }

        protected async UniTask<T> LoadComponentFromAssetGameObject<T>(string assetKey, CancellationToken cancellationToken)  
            where T : Object
        {
            var loadedAsset = await _assetProvider.LoadAsync<GameObject>(assetKey);
            
            if (cancellationToken.IsCancellationRequested)
            {
                _assetProvider.ReleaseAsset(loadedAsset);
                return null;
            }

            _cachedAssets.Add(loadedAsset);
            return loadedAsset.GetComponent<T>();
        }

        public void Dispose()
        {
            if (_cachedAssets.Count > 0)
            {
                ReleaseObjects(_cachedAssets);
                _cachedAssets.Clear();
            }

            foreach (string cachedGroup in _cachedGroups)
                _assetProvider.ReleaseGroup(cachedGroup);
            
            Resources.UnloadUnusedAssets();
            GC.Collect();
        }

        private void ReleaseObjects<T>(List<T> objects)
        {
            foreach (T obj in objects)
            {
                try
                {
                    _assetProvider.ReleaseAsset(obj);
                }
                catch (Exception e)
                {
                    Debug.LogError(obj);
                    throw;
                }
            }
        }
    }
}