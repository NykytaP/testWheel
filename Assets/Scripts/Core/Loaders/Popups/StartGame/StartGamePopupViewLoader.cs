using System.Threading;
using Core.Views.Popups.StartGame;
using Cysharp.Threading.Tasks;
using Infrastructure.AssetsManagement;
using Infrastructure.Helpers.Loaders;

namespace Core.Loaders.Popups.StartGame
{
    public class StartGamePopupViewLoader : LoaderBase, IStartGamePopupViewLoader
    {
        private const string PopupViewPath = "Popups/StartGamePopup";
        
        public StartGamePopupViewLoader(IAssetProvider assetsProvider)
            : base(assetsProvider)
        {
        }

        public UniTask<StartGamePopupView> LoadPopupView(CancellationToken cancellationToken)
        {
            return LoadComponentFromAssetGameObject<StartGamePopupView>(PopupViewPath, cancellationToken);
        }
    }
}