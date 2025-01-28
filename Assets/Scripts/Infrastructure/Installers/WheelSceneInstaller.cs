using Core.Loaders.MainUI;
using Core.Loaders.Popups.Wheel;
using Core.Services.BalanceService;
using Core.Services.PrizeGenerator;
using Core.Services.SaveLoadService;
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
            Container.Bind<IPrizeGenerator>().To<PrizeGenerator>().AsSingle();
            Container.Bind<IBalanceService>().To<BalanceService>().AsSingle();
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