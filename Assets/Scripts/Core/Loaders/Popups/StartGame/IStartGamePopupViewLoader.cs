using System.Threading;
using Core.Views.Popups.StartGame;
using Cysharp.Threading.Tasks;
using Infrastructure.Helpers.Loaders;

namespace Core.Loaders.Popups.StartGame
{
    public interface IStartGamePopupViewLoader : ILoader
    {
        public UniTask<StartGamePopupView> LoadPopupView(CancellationToken cancellationToken);
    }
}