using Core.Loaders.MainUI;
using Core.Loaders.Popups.Wheel;
using Core.ViewPresenters.Popups.StartGamePopup;
using Core.ViewPresenters.Popups.WheelPopup;
using Infrastructure.Factories;
using Infrastructure.Helpers.CancellationTokenHelper;
using Infrastructure.Helpers.GameObjectHelper;
using Zenject;

namespace Infrastructure.Installers
{
    public class WheelSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ICancellationTokenHelper>().To<CancellationTokenHelper>().AsTransient();

            BindFactories();
            BindServices();
            BindLoaders();
            BindPresenters();
        }

        private void BindFactories()
        {
            Container.BindInterfacesAndSelfTo<UIFactory>().AsCached();
        }

        private void BindServices()
        {
            Container.Bind<IGameObjectHelper>().To<GameObjectHelper>().AsSingle();
        }

        private void BindLoaders()
        {
            Container.Bind<IMainUILoader>().To<MainUILoader>().AsCached();
            Container.Bind<IWheelPopupViewLoader>().To<WheelPopupViewLoader>().AsCached();
        }

        private void BindPresenters()
        {
            Container.Bind<IWheelPopupViewPresenter>().To<WheelPopupViewPresenter>().AsTransient();
        }
    }
}