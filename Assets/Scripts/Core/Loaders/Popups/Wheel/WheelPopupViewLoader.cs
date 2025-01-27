using System.Threading;
using Core.Views.Popups.Wheel;
using Cysharp.Threading.Tasks;
using Infrastructure.AssetsManagement;
using Infrastructure.Helpers.Loaders;

namespace Core.Loaders.Popups.Wheel
{
    public class WheelPopupViewLoader : LoaderBase, IWheelPopupViewLoader
    {
        private const string PopupViewPath = "Popups/WheelPopup";
        
        public WheelPopupViewLoader(IAssetProvider assetsProvider)
            : base(assetsProvider)
        {
        }

        public UniTask<WheelPopupView> LoadPopupView(CancellationToken cancellationToken)
        {
            return LoadComponentFromAssetGameObject<WheelPopupView>(PopupViewPath, cancellationToken);
        }
    }
}