using Core.Loaders.MainUI;
using Core.Loaders.Popups.StartGame;
using Core.ViewPresenters.Popups.StartGamePopup;
using Infrastructure.Factories;
using Infrastructure.Helpers.CancellationTokenHelper;
using Infrastructure.Helpers.GameObjectHelper;
using Zenject;

namespace Infrastructure.Installers
{
    public class MenuSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ICancellationTokenHelper>().To<CancellationTokenHelper>().AsTransient();

            BindFactories();
            BindLoaders();
            BindServices();
            BindPresenters();
        }
        
        private void BindFactories()
        {
            Container.BindInterfacesAndSelfTo<UIFactory>().AsCached();
        }

        private void BindLoaders()
        {
            Container.Bind<IMainUILoader>().To<MainUILoader>().AsCached();
            Container.Bind<IStartGamePopupViewLoader>().To<StartGamePopupViewLoader>().AsCached();
        }

        private void BindServices()
        {
            Container.Bind<IGameObjectHelper>().To<GameObjectHelper>().AsSingle();
        }
        
        private void BindPresenters()
        {
            Container.Bind<IStartGamePopupViewPresenter>().To<StartGamePopupViewPresenter>().AsTransient();
        }
    }
}