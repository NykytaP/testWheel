using System.Threading;
using Core.Views.Popups.StartGame;
using Core.Views.Popups.Wheel;
using Cysharp.Threading.Tasks;
using Infrastructure.Helpers.Loaders;

namespace Core.Loaders.Popups.Wheel
{
    public interface IWheelPopupViewLoader : ILoader
    {
        public UniTask<WheelPopupView> LoadPopupView(CancellationToken cancellationToken);
    }
}