using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Infrastructure.AssetsManagement
{
    public interface IAssetProvider
    {
        UniTask<T> LoadAsync<T>(string assetKey);
        public UniTask<List<T>> LoadGroupAsync<T>(string groupKey);
        UniTask LoadSceneAsync(string sceneKey);
        void ReleaseAsset<T>(T loadedResource);
        public void ReleaseGroup(string groupKey);
    }
}