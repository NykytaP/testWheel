using Core.Loaders.MainUI;
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
        }

        private void BindServices()
        {
            Container.Bind<IGameObjectHelper>().To<GameObjectHelper>().AsSingle();
        }
        
        private void BindPresenters()
        {
        }
    }
}