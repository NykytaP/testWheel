using System.Threading;
using Cysharp.Threading.Tasks;

namespace Core.ViewPresenters.Popups.BalancePanelViewPresenter
{
    public interface IBalancePanelViewPresenter
    {
        public UniTask ShowPanel(CancellationToken cancellationToken);
    }
}