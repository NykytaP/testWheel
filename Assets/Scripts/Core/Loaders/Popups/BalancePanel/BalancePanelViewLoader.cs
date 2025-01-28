using System.Threading;
using Core.Views.Popups.BalancePanel;
using Cysharp.Threading.Tasks;
using Infrastructure.AssetsManagement;
using Infrastructure.Helpers.Loaders;

namespace Core.Loaders.Popups.BalancePanel
{
    public class BalancePanelViewLoader : LoaderBase, IBalancePanelViewLoader
    {
        private const string PanelViewPath = "Popups/BalancePanel";
        
        public BalancePanelViewLoader(IAssetProvider assetsProvider)
            : base(assetsProvider)
        {
        }

        public UniTask<BalancePanelView> LoadPanelView(CancellationToken cancellationToken)
        {
            return LoadComponentFromAssetGameObject<BalancePanelView>(PanelViewPath, cancellationToken);
        }
    }
}