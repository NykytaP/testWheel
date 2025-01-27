using System.Threading;
using Cysharp.Threading.Tasks;

namespace Core.ViewPresenters.Popups.StartGamePopup
{
    public interface IStartGamePopupViewPresenter
    {
        public UniTask ShowPopup(CancellationToken cancellationToken);
    }
}