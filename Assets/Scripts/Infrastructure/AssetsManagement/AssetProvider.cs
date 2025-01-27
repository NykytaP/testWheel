using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Infrastructure.AssetsManagement
{
    public class AssetProvider : IAssetProvider
    {
        private Dictionary<string, List<AsyncOperationHandle>> _cachedHandlers = new();
        
        public UniTask<T> LoadAsync<T>(string assetKey)
        {
            return Addressables.LoadAssetAsync<T>(assetKey).ToUniTask();
        }
        
        public async UniTask<List<T>> LoadGroupAsync<T>(string groupKey)
        {
            List<T> loadedAssets = new List<T>();

            AsyncOperationHandle<IList<T>> handler = Addressables.LoadAssetsAsync<T>(groupKey, asset =>
            {
                loadedAssets.Add(asset);
            });

            if (!_cachedHandlers.TryGetValue(groupKey, out List<AsyncOperationHandle> cachedList))
                _cachedHandlers.Add(groupKey, new List<AsyncOperationHandle>());
            
            _cachedHandlers[groupKey].Add(handler);
            
            await handler.ToUniTask();

            return loadedAssets;
        }

        public UniTask LoadSceneAsync(string sceneKey)
        {
            return Addressables.LoadSceneAsync(sceneKey).ToUniTask();
        }

        public void ReleaseAsset<T>(T loadedResource)
        {
            Addressables.Release(loadedResource);
        }
        
        public void ReleaseGroup(string groupKey)
        {
            if (_cachedHandlers.TryGetValue(groupKey, out List<AsyncOperationHandle> cachedList))
            {
                foreach (AsyncOperationHandle asyncOperationHandle in cachedList)
                    Addressables.Release(asyncOperationHandle);

                _cachedHandlers.Remove(groupKey);
            }
        }
    }
}