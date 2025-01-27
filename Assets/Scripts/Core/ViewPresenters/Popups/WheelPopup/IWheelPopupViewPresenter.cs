using System.Threading;
using Cysharp.Threading.Tasks;

namespace Core.ViewPresenters.Popups.WheelPopup
{
    public interface IWheelPopupViewPresenter
    {
        public UniTask ShowPopup(CancellationToken cancellationToken);
    }
}