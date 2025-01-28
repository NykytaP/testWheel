using System.Threading;
using Core.Views.Popups.BalancePanel;
using Cysharp.Threading.Tasks;
using Infrastructure.Helpers.Loaders;

namespace Core.Loaders.Popups.BalancePanel
{
    public interface IBalancePanelViewLoader : ILoader
    {
        public UniTask<BalancePanelView> LoadPanelView(CancellationToken cancellationToken);
    }
}