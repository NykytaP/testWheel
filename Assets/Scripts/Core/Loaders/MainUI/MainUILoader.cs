using System.Threading;
using Cysharp.Threading.Tasks;
using Infrastructure.AssetsManagement;
using Infrastructure.Helpers.Loaders;
using UnityEngine;

namespace Core.Loaders.MainUI
{
    public class MainUILoader : LoaderBase, IMainUILoader
    {
        private const string UIRootPath = "UI/UIRootPrefab";

        public MainUILoader(IAssetProvider assetsProvider)
            : base(assetsProvider)
        {
        }
        
        public UniTask<Canvas> LoadUIRoot()
        {
            return LoadComponentFromAssetGameObject<Canvas>(UIRootPath, new CancellationToken());
        }
    }
}